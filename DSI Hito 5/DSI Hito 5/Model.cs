using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Hito_5
{
    class Aldeano
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
}
