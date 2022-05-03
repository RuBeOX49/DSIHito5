using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DSI_Hito_5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Villagers : Page
    {
        public ObservableCollection<VMAldeano> VMaldeanos = new ObservableCollection<VMAldeano>();

        public ObservableCollection<ContentControl> CCAldeanos = new ObservableCollection<ContentControl>();
        public Villagers()
        {
            this.InitializeComponent();
            foreach(Aldeano aldeano in Model.GetAllAldeanos())
            {
                VMAldeano VMaldeano = new VMAldeano(aldeano);
                VMaldeanos.Add(VMaldeano);
                CCAldeanos.Add(VMaldeano.CC);
            }
            


        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlayingPage));
        }

    }
}
