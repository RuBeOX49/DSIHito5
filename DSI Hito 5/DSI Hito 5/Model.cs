using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace DSI_Hito_5
{
    public class Aldeano
    {
        public int Id { get; set; }
        public String Nombre{ get; set; }
        public string Imagen { get; set; }
        

        public Aldeano() { }



    }

    class Mejora
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


        public Mejora() { }



    }

    class Model
    {
        public static List<Aldeano> listaAldeanos = new List<Aldeano>()
        {
            new Aldeano()
            {
              Id = 0,
              Nombre = "Aldeano raso",
              Imagen = "Assets\\Images\\AldeanoRaso.png"
            },
            new Aldeano()
            {
              Id = 1,
              Nombre = "Aldeano minero",
              Imagen = "Assets\\Images\\AldeanoMinero.png"
            },
            new Aldeano()
            {
              Id = 2,
              Nombre = "Aldeano soldado",
              Imagen = "Assets\\Images\\AldeanoSoldado.png"
            },
            new Aldeano()
            {
              Id = 3,
              Nombre = "CEO",
              Imagen = "Assets\\Images\\AldeanoCEO.png"
            },
            new Aldeano()
            {
              Id = 4,
              Nombre = "Aldeano recolector",
              Imagen = "Assets\\Images\\AldeanoRecolector.png"
            },

        };

        public static IList<Aldeano> GetAllAldeanos()
        {
            return listaAldeanos;
        }

        public static Aldeano GetAldeanoById(int id)
        {
            return listaAldeanos[id];
        }




        public static List<Mejora> listaMejoras = new List<Mejora>()
        {
            new Mejora()
            {
              Id = 0,
              Nombre = "Mejora 0",
              Descripcion = "Mejora 0, descripcion"
            },
            new Mejora()
            {
              Id = 1,
              Nombre = "Mejora 1",
              Descripcion = "Mejora 1, descripcion"
            },
            new Mejora()
            {
              Id = 2,
              Nombre = "Mejora 2",
              Descripcion = "Mejora 2, descripcion"
            },
            new Mejora()
            {
              Id = 3,
              Nombre = "Mejora 3",
              Descripcion = "Mejora 3, descripcion"
            },
            new Mejora()
            {
              Id = 4,
              Nombre = "Mejora 4",
              Descripcion = "Mejora 4, descripcion"
            },
            new Mejora()
            {
              Id = 5,
              Nombre = "Mejora 5",
              Descripcion = "Mejora 5, descripcion"
            },
            new Mejora()
            {
              Id = 6,
              Nombre = "Mejora 6",
              Descripcion = "Mejora 6, descripcion"
            }

        };

        public static IList<Mejora> GetAllMejoras()
        {
            return listaMejoras;
        }

        public static Mejora GetMejoraById(int id)
        {
            return listaMejoras[id];
        }


        

    }

    public class VMAldeano : Aldeano
    {
        public Image Img;
        public ContentControl CC;
        public Grid VillagerGrid;
        
        public VMAldeano(Aldeano aldeano) 
        {
            Id = aldeano.Id;
            Nombre = aldeano.Nombre;
            Imagen = aldeano.Imagen;

            VillagerGrid = new Grid();
            /*
             Cosas que meterle a este grid:

            - Nombre del aldeano

            - Imagen del aldeano

            - Cantidad de aldeanos? TBD

            - 
             
             
             
             
             */
            // Definiciones del espacio de Grid;

            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            ColumnDefinition col3 = new ColumnDefinition();

            col1.MaxWidth = 10;
            col3.MaxWidth = 10;
            
            VillagerGrid.ColumnDefinitions.Add(col1);
            VillagerGrid.ColumnDefinitions.Add(col2);
            VillagerGrid.ColumnDefinitions.Add(col3);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();

            row1.MaxHeight = 10;
            row3.MaxHeight = 10;

            VillagerGrid.RowDefinitions.Add(row1);
            VillagerGrid.RowDefinitions.Add(row2);
            VillagerGrid.RowDefinitions.Add(row3);

            // Contenido del VillagerGrid
            
            TextBlock IDText = new TextBlock();
            IDText.Text = Id.ToString();
            IDText.FontSize = 8;

            VillagerGrid.Children.Add(IDText);
            IDText.SetValue(Grid.ColumnProperty, 0);
            IDText.SetValue(Grid.RowProperty, 0);

            TextBlock NameText = new TextBlock();
            NameText.Text = Nombre.ToString();
            NameText.FontSize = 10;

            VillagerGrid.Children.Add(NameText);
            NameText.SetValue(Grid.ColumnProperty, 1);
            NameText.SetValue(Grid.RowProperty, 2);


            Img = new Image();
            string s = System.IO.Directory.GetCurrentDirectory() + "\\" + aldeano.Imagen;
            Img.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));

            VillagerGrid.Children.Add(Img);
            Img.SetValue(Grid.ColumnProperty, 1);
            Img.SetValue(Grid.RowProperty, 1);

            CC = new ContentControl();

            CC.Content = VillagerGrid;
            CC.UseSystemFocusVisuals = true;

        
        }
    
    }
}
