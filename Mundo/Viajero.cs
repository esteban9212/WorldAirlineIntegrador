using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Mundo
{
    public class Viajero
    {
        private string id;
        private string nombre;
        private string apellido;
        private Ruta itinerario;
        

        public Viajero(string ident, string nom, string ape, Hashtable destinos)
        {
            id = ident;
            nombre = nom;
            apellido = ape;
            itinerario = new Ruta(destinos);
        }
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }
        public string Apellido
        {
            get
            {
                return apellido;
            }
            set
            {
                apellido = value;
            }
        }
        public Ruta Itinerario
        {
            get
            {
                return itinerario;
            }
            set
            {
                itinerario = value;
            }
        }

        

        public List<Ciudad> itinerarioEficiente()
        {
            itinerario.kruskal();
            return itinerario.rutaViajero(0);
        }
        public List<Arista<Ciudad>> itinerarioLibre()
        {
            List<Arista<Ciudad>> elItinerario = new List<Arista<Ciudad>>();
            return elItinerario;
        }
        
    }
}
