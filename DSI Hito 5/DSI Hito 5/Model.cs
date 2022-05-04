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
        public int warPoints { get; set; }
        public int workPoints { get; set; }


        public Aldeano() { }



    }

    class Mejora
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public bool Desbloqueada { get; set; }
        public bool Comprada { get; set; }


        public Mejora() { }



    }

    class Model
    {
        public static List<Aldeano> listaAldeanos = new List<Aldeano>()
        {
            new Aldeano()
            {
              Id = 0,
              Nombre = "Aldeano Común",
              Imagen = "Assets\\aldeanoComun.PNG",
              warPoints = 1,
              workPoints = 1
            },
            new Aldeano()
            {
              Id = 1,
              Nombre = "Aldeano Minero",
              Imagen = "Assets\\aldeanoMinero.PNG",
              warPoints = 1,
              workPoints = 2
            },
           
            new Aldeano()
            {
              Id = 2,
              Nombre = "Aldeano CEO",
              Imagen = "Assets\\AldeanoCEO.PNG",
              warPoints = 1,
              workPoints = 2
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
              Descripcion = "Mejora 0, descripcion",
              Precio = 1,
              Desbloqueada = true,
              Comprada = true
    },
            new Mejora()
            {
              Id = 1,
              Nombre = "Mejora 1",
              Descripcion = "Mejora 1, descripcion",
              Precio = 5,
              Desbloqueada = true,
              Comprada = false
            },
            new Mejora()
            {
              Id = 2,
              Nombre = "Mejora 2",
              Descripcion = "Mejora 2, descripcion",
              Precio = 10,
              Desbloqueada = false,
              Comprada = false
            },
            new Mejora()
            {
              Id = 3,
              Nombre = "Mejora 3",
              Descripcion = "Mejora 3, descripcion",
              Precio = 4,
              Desbloqueada = true,
              Comprada = false
            },
            new Mejora()
            {
              Id = 4,
              Nombre = "Mejora 4",
              Descripcion = "Mejora 4, descripcion",
              Precio = 7,
              Desbloqueada = false,
              Comprada = false
            },
            new Mejora()
            {
              Id = 5,
              Nombre = "Mejora 5",
              Descripcion = "Mejora 5, descripcion",
              Precio = 2,
              Desbloqueada = true,
              Comprada = false
            },
            new Mejora()
            {
              Id = 6,
              Nombre = "Mejora 6",
              Descripcion = "Mejora 6, descripcion",
              Precio = 3,
              Desbloqueada = false,
              Comprada = false
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
        public int warPoints;
        public int workPoints;
        
        public VMAldeano(Aldeano aldeano) 
        {
            Id = aldeano.Id;
            Nombre = aldeano.Nombre;
            Imagen = aldeano.Imagen;
            warPoints = aldeano.warPoints;
            workPoints = aldeano.workPoints;

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
            col2.MaxWidth = int.MaxValue;
            col3.MaxWidth = 10;
            
            VillagerGrid.ColumnDefinitions.Add(col1);
            VillagerGrid.ColumnDefinitions.Add(col2);
            VillagerGrid.ColumnDefinitions.Add(col3);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();

            row1.MaxHeight = 10;
            row2.MaxHeight = int.MaxValue;
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
            Img.Height = 50;
            Img.Width = 50;
            

            VillagerGrid.Children.Add(Img);
            Img.SetValue(Grid.ColumnProperty, 1);
            Img.SetValue(Grid.RowProperty, 1);

            CC = new ContentControl();

            CC.Content = VillagerGrid;
            CC.UseSystemFocusVisuals = true;
            CC.Name = Id.ToString();

        
        }
    
    }
}
