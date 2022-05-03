using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DSI_Hito_5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Upgrades : Page, INotifyPropertyChanged
    {
        public int selectedUpgrade;

        public event PropertyChangedEventHandler PropertyChanged;

        public Upgrades()
        {
            this.InitializeComponent();
            selectedUpgrade = 0;
            UpgradeNameText.Text = "";
            UpgradeDescriptionText.Text = "";
            UpgradePriceText.Text = "";
            PropertyChanged += SetUpgradeInfo;
        }

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

        public interface INotifyPropertyChanged
        {
            event PropertyChangedEventHandler PropertyChanged;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlayingPage));
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
    }
}
