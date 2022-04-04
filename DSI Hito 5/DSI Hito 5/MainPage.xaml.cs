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
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace DSI_Hito_5
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            var shadowColor = (Resources["ApplicationForegroundThemeBrush"] as SolidColorBrush).Color;
            var compositor = ElementCompositionPreview.GetElementVisual(PlayButton).Compositor;

            var dropShadow = compositor.CreateDropShadow();
            dropShadow.Color = shadowColor;
            dropShadow.BlurRadius = 20;
            dropShadow.Opacity = 30.0f;

            // var mask = PlayButton.GetAlphaMask();
            // Mejor dejar Shadows para luego...
        }
    }
}
