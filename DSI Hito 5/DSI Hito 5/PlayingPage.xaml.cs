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
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<VMAldeano> VMaldeanos = new ObservableCollection<VMAldeano>();

        public ObservableCollection<ContentControl> CCAldeanos = new ObservableCollection<ContentControl>();

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

        }

        private void EndTurnButton_Click(object sender, RoutedEventArgs e)
        {
            turnNumber++;
            moneyCount += moneyPerRound;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(turnNumber)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(moneyCount)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(openMenu)));
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
                BuyButton.Visibility = Visibility.Visible;
            }
            else
            {
                BuyButton.Visibility = Visibility.Collapsed;

                if (Model.GetMejoraById(selectedUpgrade).Comprada)
                {
                    UpgradePriceText.Text = "Comprada!";
                }
                else
                {
                    UpgradePriceText.Text = "Bloqueada";
                }
            }
        }


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


        private void BuyButton_Click(object sender, RoutedEventArgs e)
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

            // Gastar dinero
        }

        private void Node_Dragover(object sender, DragEventArgs e)
        {

        }
    }
}
