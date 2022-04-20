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

    }
}
