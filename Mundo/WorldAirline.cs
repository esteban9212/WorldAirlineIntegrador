using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundo
{
    public class WorldAirline
    {

        private int cantidadViajerosPorpagina = 1000;
        private long cantidadCasos;
        private Ruta grafo;


        


        public int CANTIDAD_LOTE
        {
            get
            {
                return cantidadViajerosPorpagina;
            }

            set
            {
                cantidadViajerosPorpagina = value;
            }
        }
        /*
        * relacion de la clase
        */
        private Hashtable viajeros;
        private Hashtable paises;
        private string rutaViajeros;
        private List<Viajero> registroTemp;


        /*
        * constructor de la clase
        */
        public WorldAirline()
        {
            paises = new Hashtable();
            viajeros = new Hashtable();
            registroTemp = new List<Viajero>();
        }

        public long CantidadLineas(string ruta)
        {
            long cantidad = 0;


            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(ruta);


            while ((line = file.ReadLine()) != null)
            {
                cantidad++;
            }
            file.Close();
            //cantidad = x;
            return cantidad;
        }



        /*
        * metodos de la clase
        */
        public void cargarCiudades(string ruta)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(ruta);


            while ((line = file.ReadLine()) != null)
            {
                string[] linea = line.Split(',');
                if (linea.Length == 7)
                {

                    int pob;
                    try
                    {
                        pob = int.Parse(linea[4]);
                    }
                    catch
                    {
                        pob = 0;
                    }

                    string pais = linea.ElementAt(0);
                    if (!paises.ContainsKey(pais))
                    {
                        paises.Add(pais, new Pais(pais, new Hashtable()));
                    }
                    Pais elPais = (Pais)paises[pais];
                    string nom = linea[1];

                    if (!elPais.Ciudades.ContainsKey(nom))
                    {
                        string lat = linea[5];
                        lat = lat.Replace('.', ',');
                        string lon = linea[6];
                        lon = lon.Replace('.', ',');

                        Ciudad nuevaCiuidad = new Ciudad(nom, nom, Convert.ToDouble(lon), Convert.ToDouble(lat), pob);

                        elPais.Ciudades.Add(nuevaCiuidad.Id, nuevaCiuidad);
                    }

                }
                else
                {
                    throw new Exception("Formato incorrecto");
                }
            }

            file.Close();

        }


        public void cargarViajeros(string ruta, int limInferior, int limSuperior)
        {
            rutaViajeros = ruta;
            viajeros = new Hashtable();

            string line;
            //if(registroTemp.Count > 0)
            //{
            //    for(int j = 0; j < registroTemp.Count; j++)
            //    {
            //        Viajero v = registroTemp.ElementAt(j);
            //        viajeros.Add(v.Id, v);
            //    }
            //}
            System.IO.StreamReader file = new System.IO.StreamReader(ruta);
            int i = 0;
            while ((line = file.ReadLine()) != null && i <= limSuperior)
            {
                if (i >= limInferior)
                {
                    string[] linea = line.Split('\t');
                    if (linea.Length == 4)
                    {
                        string id = linea.ElementAt(0);
                        if (!viajeros.ContainsKey(id))
                        {
                            string apellido = linea.ElementAt(1);
                            string nombre = linea.ElementAt(2);
                            string[] susCiudades = linea.ElementAt(3).Split(',');
                            Hashtable lasCiudades = new Hashtable();
                            foreach (string ciudad in susCiudades)
                            {

                                string[] infoCiudad = ciudad.Split('_');
                                string myCountry = infoCiudad.ElementAt(0);
                                string myCity = infoCiudad.ElementAt(1);
                                Pais p = (Pais)paises[myCountry];
                                Ciudad c = (Ciudad)p.Ciudades[myCity];
                                if (c != null)
                                {
                                    if (!lasCiudades.ContainsKey(c.Nombre))
                                    {
                                        Ciudad ci = new Ciudad(c.Nombre, c.Id, c.Longitud, c.Latitud, c.TotalPoblacion);
                                        lasCiudades.Add(ci.Nombre, ci);
                                    }
                                }
                            }

                            viajeros.Add(id, new Viajero(id, nombre, apellido, lasCiudades));
                        }

                    }
                    else
                    {
                        throw new Exception("Formato incorrecto");
                    }

                }
                i++;
            }

            file.Close();

        }

        //busca viajeros que concuerden con el nombre o con el apellido y los agrega a la lista, si encuentra
        // un viajero que concuerde con el nombre y apellido juntos, elimina todo lo que habia en la lista y agrega
        // el viajero que concordó con el nombre completo, y de alli solo sigue buscando viajeros que concuerden con
        // el nombre completo por si llegara  a haber mas viajeros con el nombre completo
        public List<Viajero> buscarViajeros(string nomViajero)
        {
            if (registroTemp.Count > 0)
            {
                for (int i = 0; i < registroTemp.Count; i++)
                {
                    viajeros.Remove(registroTemp.ElementAt(i).Id);
                }
                registroTemp.Clear();
            }
            List<Viajero> lista = new List<Viajero>();

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(rutaViajeros);

            while ((line = file.ReadLine()) != null)
            {

                string[] linea = line.Split('\t');
                string id = linea.ElementAt(0);
                if (!viajeros.ContainsKey(id))
                {
                    string apellido = linea.ElementAt(1);
                    string nombre = linea.ElementAt(2);
                    string nombreCompleto = nombre + " " + apellido;
                    Boolean encontroNomCompleto = false;
                    if (nombreCompleto.ToLower().Equals(nomViajero.ToLower()))
                    {
                        if (encontroNomCompleto == false)
                        {
                            lista.Clear();
                        }
                        encontroNomCompleto = true;
                        string[] susCiudades = linea.ElementAt(3).Split(',');
                        Hashtable lasCiudades = new Hashtable();
                        foreach (string ciudad in susCiudades)
                        {

                            string[] infoCiudad = ciudad.Split('_');
                            Pais p = (Pais)paises[infoCiudad.ElementAt(0)];
                            Ciudad c = (Ciudad)p.Ciudades[infoCiudad.ElementAt(1)];
                            Ciudad ci = new Ciudad(c.Nombre, c.Id, c.Longitud, c.Latitud, c.TotalPoblacion);
                            lasCiudades.Add(ci.Nombre, ci);
                        }
                        Viajero v = new Viajero(id, nombre, apellido, lasCiudades);
                        lista.Add(v);
                        viajeros.Add(v.Id, v);
                        registroTemp.Add(v);
                    }
                    else
                    {
                        if ((nombre.ToLower().Equals(nomViajero.ToLower()) || apellido.ToLower().Equals(nomViajero.ToLower())) && !encontroNomCompleto)
                        {

                            string[] susCiudades = linea.ElementAt(3).Split(',');
                            Hashtable lasCiudades = new Hashtable();
                            foreach (string ciudad in susCiudades)
                            {

                                string[] infoCiudad = ciudad.Split('_');
                                string myCountry = infoCiudad.ElementAt(0);
                                string myCity = infoCiudad.ElementAt(1);
                                Pais p = (Pais)paises[myCountry];
                                Ciudad c = (Ciudad)p.Ciudades[myCity];
                                if (c != null)
                                {
                                    if (!lasCiudades.ContainsKey(c.Nombre))
                                    {
                                        Ciudad ci = new Ciudad(c.Nombre, c.Id, c.Longitud, c.Latitud, c.TotalPoblacion);
                                        lasCiudades.Add(ci.Nombre, ci);
                                    }
                                }


                            }
                            Viajero v = new Viajero(id, nombre, apellido, lasCiudades);
                            lista.Add(v);
                            viajeros.Add(v.Id, v);
                            registroTemp.Add(v);

                        }
                    }
                }



            }


            return lista;
        }

        public Hashtable filtrarCiudades(int condicion)
        {
            Hashtable ciudadesFiltradas = new Hashtable();

            var paises = Paises.Values;
            foreach (Pais pais in paises)
            {

                var ciudades = pais.Ciudades.Values;
                foreach (Ciudad ciudad in ciudades)
                {
                    if (ciudad.TotalPoblacion >= condicion)
                    {
                        Ciudad ci = new Ciudad(ciudad.Nombre, ciudad.Id, ciudad.Longitud, ciudad.Latitud, ciudad.TotalPoblacion);
                        ciudadesFiltradas.Add(ci.Nombre, ci);
                    }
                }

            }
            grafo = new Ruta(ciudadesFiltradas);
            return ciudadesFiltradas;
        }

        public List<Arista<Ciudad>> formarAristasExacto()
        {
            List<Ciudad> rutasItinerarios = new List<Ciudad>();
            var cities = grafo.Ciuadades.Values;
            foreach (Ciudad c in cities)
            {
                rutasItinerarios.Add(c);
            }
            List<Ciudad> recorrido = new List<Ciudad>();
            grafo.permutaciones(rutasItinerarios, recorrido, grafo.Ciuadades.Count, grafo.Ciuadades.Count);

            List<Arista<Ciudad>> misAristas = new List<Arista<Ciudad>>();

            for (int i = 0; (i + 1) < grafo.CiudadesSolucionFuerzaBruta.Count; i++)
            {

                Ciudad inicio = grafo.CiudadesSolucionFuerzaBruta.ElementAt(i);
                Ciudad fin = grafo.CiudadesSolucionFuerzaBruta.ElementAt(i + 1);
                double distancia = grafo.Grafo[inicio.PosEnGrafo, fin.PosEnGrafo];
                Arista<Ciudad> myEdge = new Arista<Ciudad>(inicio, fin, distancia);

                misAristas.Add(myEdge);

            }
            Ciudad inicioItinerario = grafo.CiudadesSolucionFuerzaBruta.ElementAt(0);
            Ciudad finItinerario = grafo.CiudadesSolucionFuerzaBruta.ElementAt(grafo.CiudadesSolucionFuerzaBruta.Count - 1);
            Arista<Ciudad> lastUnion = new Arista<Ciudad>(inicioItinerario, finItinerario, grafo.Grafo[inicioItinerario.PosEnGrafo, finItinerario.PosEnGrafo]);
            misAristas.Add(lastUnion);



            return misAristas;
        }




        public Hashtable Paises
        {
            get
            {
                return paises;
            }

            set
            {
                paises = value;
            }
        }
        public Hashtable Viajeros
        {
            get
            {
                return viajeros;
            }

            set
            {
                viajeros = value;
            }
        }

        public string RutaViajeros
        {
            get
            {
                return rutaViajeros;
            }

            set
            {
                rutaViajeros = value;
            }
        }

        public long CantidadCasos
        {
            get
            {
                return cantidadCasos;
            }

            set
            {
                cantidadCasos = value;
            }
        }

        public Ruta Grafo
        {
            get
            {
                return grafo;
            }

            set
            {
                grafo = value;
            }
        }
    }
}
