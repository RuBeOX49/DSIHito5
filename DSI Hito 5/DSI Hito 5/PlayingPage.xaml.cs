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
        public event PropertyChangedEventHandler PropertyChanged;

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
            openMenu = Menu.None;
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
            openMenu = Menu.Upgrade;
            Frame.Navigate(typeof(Upgrades));
            // UpgradeMenu.Visibility = Visibility.Visible;
        }

        private void VillagersButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Villagers));
        }

        private void NextTurnButton_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Tab)
            {
                e.Handled = true;
                UpgradeButton.Focus(FocusState.Programmatic);
            }
        }
    }
}
