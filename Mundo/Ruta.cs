using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Mundo
{
    public class Ruta : IGrafo<Ciudad>
    {
        /*
        * atributos de la clase
        */
        private Hashtable ciudades;
        private Hashtable ciudadesGrafo;
        private double[,] grafo;
        private List<Arista<Ciudad>> aristas;
        private int[] padres;
        private int[,] control;
        private bool[,] visitados;
        private List<int> lista;
        private double distanciaMinimaCombinaciones;
        private List<Ciudad> rutaFuerzaBruta;
        /*
        * constructor de la clase
        */
        public Ruta(Hashtable ciudadesViajero)
        {
            ciudades = ciudadesViajero;
            ciudadesGrafo = new Hashtable();
            control = new int[ciudadesGrafo.Count, ciudadesGrafo.Count];
            visitados = new bool[ciudadesGrafo.Count, ciudadesGrafo.Count];
            lista = new List<int>();
            rutaFuerzaBruta = new List<Ciudad>();
            distanciaMinimaCombinaciones = double.MaxValue;
            calcularDistancias();
            control = new int[ciudadesGrafo.Count, ciudadesGrafo.Count];

        }
        /*
        * metodos de la clase
        */
        private double distancia(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(degArad(lat1)) * Math.Sin(degArad(lat2)) + Math.Cos(degArad(lat1)) * Math.Cos(degArad(lat2)) * Math.Cos(degArad(theta));
            dist = Math.Acos(dist);
            dist = radAdeg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }
        private double radAdeg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
        private double degArad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        public void setDestinos(Hashtable nDestino)
        {
            ciudades = nDestino;
        }
        public Hashtable CiuadadesGrafo
        {
            get
            {
                return ciudadesGrafo;
            }
            set
            {
                ciudadesGrafo = value;
            }
        }

        public Hashtable Ciuadades
        {
            get
            {
                return ciudades;
            }
            set
            {
                ciudades = value;
            }
        }
        public double[,] Grafo
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
        public List<Ciudad> CiudadesSolucionFuerzaBruta
        {
            get
            {
                return rutaFuerzaBruta;
            }
            set
            {
                rutaFuerzaBruta = value;
            }
        }
        public double[,] calcularDistancias()
        {
            ciudadesGrafo.Clear();
            var ciudadesN = ciudades.Values;
            int pos = 0;
            foreach (var ciudad in ciudadesN)
            {
                Ciudad miDestino = (Ciudad)(ciudad);

                miDestino.PosEnGrafo = pos;
                ciudadesGrafo.Add(pos, miDestino);
                pos++;

            }

            grafo = new double[ciudades.Count, ciudades.Count];
            for (int i = 0; i < ciudades.Count; i++)
            {
                for (int j = 0; j < ciudades.Count; j++)
                {
                    if (i == j)
                    {
                        grafo[i, j] = 0;
                    }
                    else
                    {
                        double longitud1 = ((Ciudad)(ciudadesGrafo[i])).Longitud;
                        double latitud1 = ((Ciudad)(ciudadesGrafo[i])).Latitud;
                        double longitud2 = ((Ciudad)(ciudadesGrafo[j])).Longitud;
                        double latitud2 = ((Ciudad)(ciudadesGrafo[j])).Latitud;
                        grafo[i, j] = distancia(latitud1, longitud1, latitud2, longitud2, 'K');
                    }
                }
            }
            return grafo;
        }


       

        public List<Ciudad> voraz(Ciudad city)
        {
            
            List<Ciudad> ciudadesVisitadas = new List<Ciudad>();
            int posFila = city.PosEnGrafo;
            List<int> listaEnteros = new List<int>();
            control = new int[ciudades.Count, ciudades.Count];
            buscarMenorDistancia(posFila,listaEnteros, ciudades.Count);

            foreach (int pos in listaEnteros)
            {
                Ciudad nueva = (Ciudad)ciudadesGrafo[pos];
                ciudadesVisitadas.Add(nueva);
                Console.WriteLine(nueva.Nombre);
            }


            //int posColumna = buscarMenorDistancia(posFila);
            //Ciudad inicio = (Ciudad)CiuadadesGrafo[posFila];
            //ciudadesVisitadas.Add(inicio);
            //Ciudad fin = (Ciudad)CiuadadesGrafo[posColumna];
            //ciudadesVisitadas.Add(fin);
            return ciudadesVisitadas;

        }




        private List<int> buscarMenorDistancia(int pos, List<int> lista, int n)
        {
            if(n == 0)
            {
                return lista;
            }
            lista.Add(pos);
            int posMenor = 0;
            double distanciaMenor = double.MaxValue;
            for (int i = 0; i < grafo.GetLongLength(1); i++)
            {
                if (control[pos, i] == 0 && i != pos && !lista.Contains(i))
                {

                    if (grafo[pos, i] < distanciaMenor)
                    {
                        distanciaMenor = grafo[pos, i];
                        posMenor = i;

                    }
                                  
                }
            }
            control[pos, posMenor] = 1;
            control[posMenor, pos] = 1;

            
            buscarMenorDistancia(posMenor, lista, n - 1);

            




            return lista;
        }
        public void kruskalOpcion1()
        {
            //List<Arista<Ciudad>> miRetorno = new List<Arista<Ciudad>>();  //1

            if (ciudades.Count == 1)
            {
                control = new int[ciudades.Count, ciudades.Count];  //3

                var laCiudad = ciudades.Values;
                Ciudad laUnica = null;
                foreach (var theCity in laCiudad)
                {
                    laUnica = (Ciudad)theCity;
                }
                //miRetorno.Add(new Arista<Ciudad>(laUnica, laUnica, 0));
                //return miRetorno;
                control[laUnica.PosEnGrafo, laUnica.PosEnGrafo] = 1;

            }
            else
            {
                var grafoCiudades1 = ciudades.Values;
                foreach (var c in grafoCiudades1)
                {
                    Ciudad city = (Ciudad)c;
                    city.CantidadAdyacencias = 0;
                }
                List<Arista<Ciudad>> laLista = listaOrdenada();  //2
                control = new int[ciudades.Count, ciudades.Count];  //3
                foreach (Arista<Ciudad> arista in laLista)  //4
                {
                    Ciudad origen = (Ciudad)arista.Destino1;  //5
                    Ciudad destino = (Ciudad)arista.Destino2; //6
                    control[origen.PosEnGrafo, destino.PosEnGrafo] = 1;  //7
                    control[destino.PosEnGrafo, origen.PosEnGrafo] = 1;  //8
                    bool ciclos = haycircuito(control, origen, destino);  //9
                                                                          //Console.WriteLine("------------------------------------------------------");
                                                                          //Console.WriteLine(ciclos);
                    if (!ciclos && origen.CantidadAdyacencias < 2 && destino.CantidadAdyacencias < 2 && origen.PosEnGrafo != destino.PosEnGrafo) //10
                    {
                        
                        origen.CantidadAdyacencias++;
                        destino.CantidadAdyacencias++;
                        Console.WriteLine(origen.Nombre + "-" + destino.Nombre);
                        Console.WriteLine("<--------------------------entro---------------------->");



                    }
                    else
                    {
                        control[origen.PosEnGrafo, destino.PosEnGrafo] = 0; //12
                        control[destino.PosEnGrafo, origen.PosEnGrafo] = 0; // 13
                    }
                }

               
            }

        }
        private bool haycircuito(int[,] matrizControl, Ciudad d1, Ciudad d2)
        {
            bool hayCiclo = false;  //1
            bool[] hanSidoVisitados = new bool[ciudadesGrafo.Count]; //2
            hanSidoVisitados[d1.PosEnGrafo] = true; //3
            hanSidoVisitados[d2.PosEnGrafo] = true;  //4
            Queue<int> cola = new Queue<int>(); // 5
            cola.Enqueue(d2.PosEnGrafo);  //6
            while (cola.Count > 0) //7
            {
                int i = cola.Dequeue(); //8
                for (int j = 0; j < ciudadesGrafo.Count && !hayCiclo; j++) //9
                {
                    if (control[i, j] > 0 && hanSidoVisitados[j] && i != d1.PosEnGrafo && j != d2.PosEnGrafo) //10
                    {
                        if (i != d2.PosEnGrafo && j == d1.PosEnGrafo) //11
                        {
                            hayCiclo = true; // 12
                        }
                    }
                    else
                    {
                        if (control[i, j] > 0 && j != d2.PosEnGrafo) // 13
                        {
                            cola.Enqueue(j); //14
                            hanSidoVisitados[j] = true; //15
                        }
                    }
                }
            }
            return hayCiclo; //16
        }

        private void iniciarListaAristas()
        {
            aristas = new List<Arista<Ciudad>>();                                     //1
            for (int i = 0; i < grafo.GetLongLength(0) - 1; i++)            //2
            {
                for (int j = i + 1; j < grafo.GetLongLength(1); j++)        //3
                {
                    Ciudad inicio = (Ciudad)CiuadadesGrafo[i];             //4
                    Ciudad fin = (Ciudad)CiuadadesGrafo[j];                   //5
                    double peso = grafo[i, j];                          //6
                    Arista<Ciudad> nuevaArista = new Arista<Ciudad>(inicio, fin, peso);     //7
                    aristas.Add(nuevaArista);                           //8
                }
            }
        }
        private List<Arista<Ciudad>> listaOrdenada()
        {

            iniciarListaAristas();                                          //nn
            List<Arista<Ciudad>> recorrer = aristas.OrderBy(o => o.Distancia).ToList();//nn
            return recorrer;                    //1
        }
        private void makeSet()
        {
            padres = new int[CiuadadesGrafo.Count];//1
            //colle x =DestinosGrafo.Values;
            foreach (Ciudad clave in CiuadadesGrafo.Values)//2
            {
                padres[clave.PosEnGrafo] = clave.PosEnGrafo;//3
            }
        }
        private int Find(int x)
        {
            if (x == padres[x])
            {          //Si estoy en la raiz
                return x;                   //Retorno la raiz
            }
            else return Find(padres[x]); //De otro modo busco el padre del vértice actual, hasta llegar a la raiz.
        }
        private bool sameComponent(int x, int y)
        {
            if (Find(x) == Find(y)) return true;   //si poseen la misma raíz
            return false;
        }
        private void Union(int x, int y)
        {
            int xRoot = Find(x);    //Obtengo la raiz de la componente del vértice X
            int yRoot = Find(y);    //Obtengo la raiz de la componente del vértice Y
            padres[xRoot] = yRoot;   //Mezclo ambos arboles o conjuntos, actualizando su padre de alguno de ellos como la raiz de otro
        }
        public List<Arista<Ciudad>> kruskalOpcion2()
        {
            makeSet();  //o(n)
            List<Arista<Ciudad>> ordenada = listaOrdenada();//o(nn)
            List<Arista<Ciudad>> retorno = new List<Arista<Ciudad>>();//1
            foreach (Arista<Ciudad> arista in ordenada)//on
            {
                Ciudad origen = (Ciudad)arista.Destino1;  //Vértice origen de la arista actual
                Ciudad destino = (Ciudad)arista.Destino2; //Vértice destino de la arista actual
                double peso = arista.Distancia;             //Peso de la arista actual
                                                            //Verificamos si estan o no en la misma componente conexa
                if (!sameComponent(origen.PosEnGrafo, destino.PosEnGrafo))
                {  //Evito ciclos
                    if (origen.CantidadAdyacencias < 2 && destino.CantidadAdyacencias < 2)
                    {
                        retorno.Add(arista);//Incremento el peso total del MST
                        //Agrego al MST la arista actual
                        Union(origen.PosEnGrafo, destino.PosEnGrafo);
                        origen.CantidadAdyacencias++;
                        destino.CantidadAdyacencias++;
                    }
                }
            }
            List<Ciudad> ciudadesSinUnir = new List<Ciudad>();
            var grafoCiudades = ciudadesGrafo.Values;
            foreach (var c in grafoCiudades)
            {
                Ciudad city = (Ciudad)c;
                if (city.CantidadAdyacencias < 2)
                {
                    ciudadesSinUnir.Add(city);
                }
            }
            ciudadesSinUnir.ElementAt(0).CantidadAdyacencias++;
            ciudadesSinUnir.ElementAt(1).CantidadAdyacencias++;
            Arista<Ciudad> miArista = new Arista<Ciudad>(ciudadesSinUnir.ElementAt(0), ciudadesSinUnir.ElementAt(1), Grafo[ciudadesSinUnir.ElementAt(0).PosEnGrafo, ciudadesSinUnir.ElementAt(1).PosEnGrafo]);
            retorno.Add(miArista);
            return retorno;
        }
        private void recorridoEnPreOrden(int raiz)
        {

            lista.Add(raiz);

            for (int j = 0; (j < control.GetLongLength(1)); j++)
            {
                if (control[raiz, j] != 0 && (!visitados[raiz, j]) && (raiz != j))
                {
                    visitados[raiz, j] = true;
                    visitados[j, raiz] = true;
                    recorridoEnPreOrden(j);

                }
            }

        }

        public List<Arista<Ciudad>> rutaViajero(int raiz)
        {
            List<Arista<Ciudad>> laRuta = new List<Arista<Ciudad>>();

            if (ciudades.Count > 0)
            {
                visitados = new bool[ciudadesGrafo.Count, ciudadesGrafo.Count];
                lista = new List<int>();
                recorridoEnPreOrden(raiz);
                //lista.Add(raiz);
                Console.WriteLine("<------------------------" + lista.Count + "------------------------->");
                for (int i = 0; (i + 1) < lista.Count; i++)
                {
                    Ciudad ciudad1 = (Ciudad)ciudadesGrafo[lista.ElementAt(i)];
                    Ciudad ciudad2 = (Ciudad)ciudadesGrafo[lista.ElementAt(i + 1)];
                    double distancia = grafo[ciudad1.PosEnGrafo, ciudad2.PosEnGrafo];
                    laRuta.Add(new Arista<Ciudad>(ciudad1, ciudad2, distancia));
                }

                Ciudad inicio = (Ciudad)ciudadesGrafo[lista.ElementAt(lista.Count - 1)];
                Ciudad fin = (Ciudad)ciudadesGrafo[lista.ElementAt(0)];
                double distancia1 = grafo[inicio.PosEnGrafo, fin.PosEnGrafo];
                laRuta.Add(new Arista<Ciudad>(inicio, fin, distancia1));
            }



            return laRuta;
        }


        public void permutaciones(List<Ciudad> grafoCiudades, List<Ciudad> rutaCiudades, int n, int r)
        {
            if (n == 0)
            {
                double dist = calcularPesoCamino(rutaCiudades);
                if (dist < distanciaMinimaCombinaciones)
                {
                    rutaFuerzaBruta.Clear();
                    rutaFuerzaBruta.AddRange(rutaCiudades);
                    distanciaMinimaCombinaciones = dist;
                }
            }
            else {
                for (int i = 0; i < r; i++)
                {
                    if (!rutaCiudades.Contains(grafoCiudades[i]))
                    {
                        rutaCiudades.Add(grafoCiudades[i]);

                        permutaciones(grafoCiudades, rutaCiudades, n - 1, r);
                        rutaCiudades.RemoveAt(rutaCiudades.Count - 1);
                    }
                }
            }
        }
        private double calcularPesoCamino(List<Ciudad> rutaCiudades)
        {
            double distanciaRuta = 0;
            if (rutaCiudades.Count <= 1)
            {
                return 0;
            }
            else
            {
                for (int i = 0; i < rutaCiudades.Count - 1; i++)
                {
                    distanciaRuta += Grafo[rutaCiudades[i].PosEnGrafo, rutaCiudades[i + 1].PosEnGrafo];
                }
                distanciaRuta += Grafo[rutaCiudades[rutaCiudades.Count - 1].PosEnGrafo, rutaCiudades[0].PosEnGrafo];
                return distanciaRuta;
            }
        }
    }
}
