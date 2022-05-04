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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace DSI_Hito_5
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Settings : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        double volumeSaved;
        enum graphicsAllowed { Low, Medium, High, Ultra};
        graphicsAllowed graphics;
        public Settings()
        {
            this.InitializeComponent();
            graphics = graphicsAllowed.High;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
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

        private void DecreaseGraphics_Click(object sender, RoutedEventArgs e)
        {
            if (graphics > graphicsAllowed.Low)
            {
                graphics--;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(graphics)));
            }

            
        }

        private void IncreaseGraphics_Click(object sender, RoutedEventArgs e)
        {
            if (graphics < graphicsAllowed.Ultra)
            {
                graphics++;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(graphics)));
            }
        }
    }
}
