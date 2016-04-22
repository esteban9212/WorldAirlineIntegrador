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


        public List<Arista<Ciudad>> itinerarioExploracionCompleta()
        {
            List<Arista<Ciudad>> misAristas = new List<Arista<Ciudad>>();
            if(itinerario.Ciuadades.Count > 0)
            {
                List<Ciudad> rutasItinerarios = new List<Ciudad>();
                var cities = itinerario.Ciuadades.Values;
                foreach (Ciudad c in cities)
                {
                    rutasItinerarios.Add(c);
                }
                List<Ciudad> recorrido = new List<Ciudad>();
                itinerario.permutaciones(rutasItinerarios, recorrido, itinerario.Ciuadades.Count, itinerario.Ciuadades.Count);

                for (int i = 0; (i + 1) < itinerario.CiudadesSolucionFuerzaBruta.Count; i++)
                {
                    Ciudad inicio = itinerario.CiudadesSolucionFuerzaBruta.ElementAt(i);
                    Ciudad fin = itinerario.CiudadesSolucionFuerzaBruta.ElementAt(i + 1);
                    double distancia = itinerario.Grafo[inicio.PosEnGrafo, fin.PosEnGrafo];
                    Arista<Ciudad> myEdge = new Arista<Ciudad>(inicio, fin, distancia);
                    misAristas.Add(myEdge);
                }
                Ciudad finItinerario = itinerario.CiudadesSolucionFuerzaBruta.ElementAt(0);
                Ciudad inicioItinerario = itinerario.CiudadesSolucionFuerzaBruta.ElementAt(itinerario.CiudadesSolucionFuerzaBruta.Count - 1);
                Arista<Ciudad> lastUnion = new Arista<Ciudad>(inicioItinerario, finItinerario, itinerario.Grafo[inicioItinerario.PosEnGrafo, finItinerario.PosEnGrafo]);
                misAristas.Add(lastUnion);
            }
           

            return misAristas;
        }
        public List<Arista<Ciudad>> itinerarioEficiente()
        {
            itinerario.kruskalOpcion1();
            return itinerario.rutaViajero(0);
        }
        public List<Arista<Ciudad>> itinerarioLibre()
        {
            List<Arista<Ciudad>> elItinerario = new List<Arista<Ciudad>>();
            return elItinerario;
        }
        //private List<Arista<Ciudad>> orientacionCircuito(List<Arista<Ciudad>> lista)
        //{
        //    List<Arista<Ciudad>> orientacion = new List<Arista<Ciudad>>();
        //    Ciudad principio = null;
        //    var cities = itinerario.Ciuadades.Values;
        //    foreach(var c in cities)
        //    {
        //        principio = (Ciudad)c;
        //    }
        //    for(int i = 0; i < lista.Count; i++)
        //    {
        //        if (lista.ElementAt(i).Destino1.Id.Equals(principio.Id))
        //        {
        //            orientacion
        //        }
        //    }
        //}
    }
}
