using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using Windows.System;
using Windows.UI;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Gaming.Input;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace DSI_Hito_5
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PlayingPage : Page, INotifyPropertyChanged
    {
        public int turnNumber;
        public int moneyCount;
        public int moneyPerRound;
        public int selectedUpgrade;
        double volumeSaved;

        public int NodeCount;

        public string selectedVillager;
        private bool isVillagerSelected = false;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<VMAldeano> VMaldeanos = new ObservableCollection<VMAldeano>();

        public ObservableCollection<ContentControl> CCAldeanos = new ObservableCollection<ContentControl>();

        public ObservableCollection<ContentControl> CCNodo = new ObservableCollection<ContentControl>();

        public ObservableCollection<ContentControl> CCWarNodo = new ObservableCollection<ContentControl>();

        private readonly object myLock = new object();
        List<Gamepad> myGamepads = new List<Gamepad>();
        Gamepad mainGamepad = null;
        GamepadReading reading, prereading;
        GamepadVibration vibration;

        public DispatcherTimer GamePadTimer = null;

        enum Menu
        {
            None,
            Upgrade,
            Villagers
        }

        Menu openMenu;

        public PlayingPage()
        {
            this.InitializeComponent();
            turnNumber = 1;
            moneyCount = 10;
            moneyPerRound = 5;
            selectedUpgrade = 0;
            NodeCount = 0;
            UpgradeNameText.Text = "";
            UpgradeDescriptionText.Text = "";
            PropertyChanged += SetUpgradeInfo;
            openMenu = Menu.None;
            foreach (Aldeano aldeano in Model.GetAllAldeanos())
            {
                VMAldeano VMaldeano = new VMAldeano(aldeano);
                VMaldeanos.Add(VMaldeano);
                CCAldeanos.Add(VMaldeano.CC);
            }

            Gamepad.GamepadAdded += (object sender, Gamepad e) =>
            {
                lock (myLock)
                {
                    bool gamepadInList = myGamepads.Contains(e);

                    if (!gamepadInList)
                    {
                        myGamepads.Add(e);
                    }
                }
            };

            Gamepad.GamepadRemoved += (object sender, Gamepad e) =>
            {
                lock (myLock)
                {
                    int indexRemoved = myGamepads.IndexOf(e);

                    if (indexRemoved > -1)
                    {
                        if (mainGamepad == myGamepads[indexRemoved])
                        {
                            mainGamepad = null;
                        }

                        myGamepads.RemoveAt(indexRemoved);
                    }
                }
            };

            GamePadTimerSetup();

        }

        public void GamePadTimerSetup()
        {
            GamePadTimer = new DispatcherTimer();
            GamePadTimer.Tick += GamePadTimer_Tick;// dispatcherTimer_Tick;
            GamePadTimer.Interval = new TimeSpan(100000); //100000*10^-7s=1cs;
            GamePadTimer.Start();
        }

        void GamePadTimer_Tick(object sender, object e)
        { //Función de respuesta al Timer cada 0.01s
            if (mainGamepad != null)
            {
                LeeMando(); //Lee GamePAd
                ActualizaUI();
                    
            }
        }

        private void ActualizaUI()
        {
            if (mainGamepad != null && WarNode.FocusState != FocusState.Unfocused && reading.Buttons == GamepadButtons.A) 
            {
                if (isVillagerSelected)
                {

                    var ID = selectedVillager;
                    var parsedID = int.Parse(ID);

                    VMAldeano foo = new VMAldeano(Model.GetAldeanoById(parsedID));

                    CCNodo.Add(foo.CC);
                    isVillagerSelected = false;
                }
                UpgradeButton.Focus(FocusState.Keyboard);
            }
            if (mainGamepad != null && WarNode.FocusState != FocusState.Unfocused && reading.Buttons == GamepadButtons.A)
            {
                if (isVillagerSelected)
                {

                    var ID = selectedVillager;
                    var parsedID = int.Parse(ID);

                    VMAldeano foo = new VMAldeano(Model.GetAldeanoById(parsedID));

                    CCWarNodo.Add(foo.CC);
                    isVillagerSelected = false;
                }
                UpgradeButton.Focus(FocusState.Keyboard);
            }
        }

        private void LeeMando()
        {
            if (mainGamepad != null)
            {
                prereading = reading; //por si hay algún error o para calcular la diferencia
                reading = mainGamepad.GetCurrentReading();
            }
        }

        private void EndTurnButton_Click(object sender, RoutedEventArgs e)
        {
            turnNumber++;
            moneyCount += moneyPerRound;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(turnNumber)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(moneyCount)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(openMenu)));
            CCWarNodo.Clear();
            CCNodo.Clear();
            NodeCount = 0;
            NodeTotal.Visibility = Visibility.Collapsed;
            NodeTotal.Content = "0";
            WarNodeTotal.Content = "0";
            WarNodeTotal.Visibility = Visibility.Collapsed;
            Node.Background = new SolidColorBrush(Colors.MidnightBlue);
            WarNode.Background = new SolidColorBrush(Colors.MidnightBlue);
        }
        public interface INotifyPropertyChanged
        {
            event PropertyChangedEventHandler PropertyChanged;
        }

        private void UpgradeButton_Click(object sender, RoutedEventArgs e)
        {
            //openMenu = Menu.Upgrade;
            //Frame.Navigate(typeof(Upgrades));
            UpgradesPopup.IsOpen = true;
            Upgrade0_Click(null, null);
            Upgrade0.Focus(FocusState.Keyboard);

        }

        private void VillagersButton_Click(object sender, RoutedEventArgs e)
        {
            VillagersPopup.IsOpen = true;
            
            
        }

        private void NextTurnButton_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Tab)
            {
                e.Handled = true;
                UpgradeButton.Focus(FocusState.Programmatic);
            }
        }

        // Popup

        private void SetUpgradeInfo(object sender, PropertyChangedEventArgs e)
        {
            UpgradeNameText.Text = Model.GetMejoraById(selectedUpgrade).Nombre;
            UpgradeDescriptionText.Text = Model.GetMejoraById(selectedUpgrade).Descripcion;

            if (Model.GetMejoraById(selectedUpgrade).Desbloqueada && !Model.GetMejoraById(selectedUpgrade).Comprada)
            {
                UpgradePriceText.Text = Model.GetMejoraById(selectedUpgrade).Precio.ToString();
                DollarSign.Visibility = Visibility.Visible;
                BuyButton.Visibility = Visibility.Visible;

                if (moneyCount < Model.GetMejoraById(selectedUpgrade).Precio)
                {
                    BuyButton.Background = new SolidColorBrush(Colors.DimGray);
                    UpgradePriceText.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    BuyButton.Background = new SolidColorBrush(Colors.RoyalBlue);
                    UpgradePriceText.Foreground = new SolidColorBrush(Colors.White);
                }
            }
            else
            {
                DollarSign.Visibility = Visibility.Collapsed;
                BuyButton.Visibility = Visibility.Collapsed;

                if (Model.GetMejoraById(selectedUpgrade).Comprada)
                {
                    UpgradePriceText.Foreground = new SolidColorBrush(Colors.White);
                    UpgradePriceText.Text = "Comprada!";
                }
                else
                {
                    UpgradePriceText.Foreground = new SolidColorBrush(Colors.Red);
                    UpgradePriceText.Text = "Bloqueada";
                }
            }
        }

        #region upgradebutton
        private void Upgrade0_Click(object sender, RoutedEventArgs e)
        {
            selectedUpgrade = 0;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedUpgrade)));
        }

        private void Upgrade1_Click(object sender, RoutedEventArgs e)
        {
            selectedUpgrade = 1;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedUpgrade)));
        }

        private void Upgrade2_Click(object sender, RoutedEventArgs e)
        {
            selectedUpgrade = 2;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedUpgrade)));
        }

        private void Upgrade3_Click(object sender, RoutedEventArgs e)
        {
            selectedUpgrade = 3;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedUpgrade)));
        }

        private void Upgrade4_Click(object sender, RoutedEventArgs e)
        {
            selectedUpgrade = 4;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedUpgrade)));
        }

        private void Upgrade5_Click(object sender, RoutedEventArgs e)
        {
            selectedUpgrade = 5;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedUpgrade)));
        }

        private void Upgrade6_Click(object sender, RoutedEventArgs e)
        {
            selectedUpgrade = 6;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedUpgrade)));
        }

        private SymbolIcon getUpgradeIcon(int id)
        {
            switch (id)
            {
                case 0:
                    return Icon0;
                case 1:
                    return Icon1;
                case 2:
                    return Icon2;
                case 3:
                    return Icon3;
                case 4:
                    return Icon4;
                case 5:
                    return Icon5;
                case 6:
                    return Icon6;
                default:
                    return Icon0;
            }
        }

        #endregion
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (moneyCount >= Model.GetMejoraById(selectedUpgrade).Precio)
            {
                Model.GetMejoraById(selectedUpgrade).Comprada = true;

                getUpgradeIcon(selectedUpgrade).Foreground = new SolidColorBrush(Colors.RoyalBlue);
                BuyButton.Visibility = Visibility.Collapsed;
                UpgradePriceText.Text = "Comprada!";

                if (selectedUpgrade != 6)
                {
                    Model.GetMejoraById(selectedUpgrade + 1).Desbloqueada = true;

                    if (!Model.GetMejoraById(selectedUpgrade + 1).Comprada)
                        getUpgradeIcon(selectedUpgrade + 1).Foreground = new SolidColorBrush(Colors.White);
                }
                moneyCount -= Model.GetMejoraById(selectedUpgrade).Precio;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(moneyCount)));
            }
            else NoDineroPopup.IsOpen = true;
        }

        private void Node_Dragover(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private void VillagerPopout_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            //definir en código de qué aldeano estamos hablando
            ContentControl CCAldeano = e.Items[0] as ContentControl;
            //identificar qué aldeano es
            string id = CCAldeano.Name;
            //establecer el ID del objeto
            e.Data.SetText(id);
            //establecer qué operación queremos hacer
            e.Data.RequestedOperation = DataPackageOperation.Copy;
        }

        private async void Node_DropOverEvent(object sender, DragEventArgs e)
        {
            var ID = await e.DataView.GetTextAsync();
            var parsedID = int.Parse(ID);

            VMAldeano foo = new VMAldeano(Model.GetAldeanoById(parsedID));

            CCNodo.Add(foo.CC);
            Node.Background = new SolidColorBrush(Colors.Transparent);
            NodeTotal.SetValue(VisibilityProperty, Visibility.Visible);
            NodeTotal.Content = (int.Parse((string)NodeTotal.Content) + foo.workPoints).ToString();
            NodeCount++;
        }

        private async void WarNode_DropOverEvent(object sender, DragEventArgs e)
        {
            var ID = await e.DataView.GetTextAsync();
            var parsedID = int.Parse(ID);

            VMAldeano foo = new VMAldeano(Model.GetAldeanoById(parsedID));

            CCWarNodo.Add(foo.CC);
            WarNode.Background = new SolidColorBrush(Colors.Transparent);
            WarNodeTotal.SetValue(VisibilityProperty, Visibility.Visible);
            WarNodeTotal.Content = (int.Parse((string)WarNodeTotal.Content) + foo.warPoints).ToString();
        }

        private void VillagerPopup_ItemClick(object sender, ItemClickEventArgs e)
        {
            ContentControl CCAldeano = e.ClickedItem as ContentControl;
            //identificar qué aldeano es y establecer el ID del objeto
            selectedVillager = CCAldeano.Name;
            //llamar al selector de nodo????
            Node.Focus(FocusState.Keyboard);
        }

        private void Node_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WarNode_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(isVillagerSelected)
            {
                
                var ID = selectedVillager;
                var parsedID = int.Parse(ID);

                VMAldeano foo = new VMAldeano(Model.GetAldeanoById(parsedID));

                CCWarNodo.Add(foo.CC);
                isVillagerSelected = false;
            }
           
            UpgradeButton.Focus(FocusState.Keyboard);
        }

        private void Node_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == VirtualKey.A)
            {
            if (isVillagerSelected)
            {

                var ID = selectedVillager;
                var parsedID = int.Parse(ID);

                VMAldeano foo = new VMAldeano(Model.GetAldeanoById(parsedID));

                CCNodo.Add(foo.CC);
                isVillagerSelected = false;
            }
            UpgradeButton.Focus(FocusState.Keyboard);

            }
            if (e.Key == VirtualKey.GamepadLeftThumbstickDown || e.Key == VirtualKey.GamepadLeftThumbstickLeft || e.Key == VirtualKey.GamepadLeftThumbstickRight || e.Key == VirtualKey.GamepadLeftThumbstickUp) 
            {
                WarNode.Focus(FocusState.Keyboard);
            }

            e.Handled = true;
        }

        private void WarNode_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (isVillagerSelected)
            {

                var ID = selectedVillager;
                var parsedID = int.Parse(ID);

                VMAldeano foo = new VMAldeano(Model.GetAldeanoById(parsedID));

                CCWarNodo.Add(foo.CC);
                isVillagerSelected = false;
            }
            if (e.Key == VirtualKey.GamepadLeftThumbstickDown || e.Key == VirtualKey.GamepadLeftThumbstickLeft || e.Key == VirtualKey.GamepadLeftThumbstickRight || e.Key == VirtualKey.GamepadLeftThumbstickUp)
            {
                Node.Focus(FocusState.Keyboard);
            }
            UpgradeButton.Focus(FocusState.Keyboard);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            PausePopup.IsOpen = true;
        }

        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            if (VolumeAdjust.Value > 0)
            {
                volumeSaved = VolumeAdjust.Value;
                VolumeAdjust.Value = 0;
            }
            else VolumeAdjust.Value = volumeSaved;
        }

        private void YesSurrender_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void NoSurrender_Click(object sender, RoutedEventArgs e)
        {
            PausePopup.IsOpen = false;
        }

        private void SurrenderButton_Click(object sender, RoutedEventArgs e)
        {
            SurrenderPopup.IsOpen = true;
        }
    }
}
