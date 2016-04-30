using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mundo;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace AerolineaTest
{
    [TestClass]
    public  class TestViajero
    {

        private WorldAirline w1;
        private WorldAirline w2;
        private WorldAirline w3;
        private Hashtable destinos;
       
        private static string ruta11()
        {
            string rutax = "";
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string gg = outPutDirectory + "\\NuevasCiudades.txt";
            string tt = gg.Trim();
            rutax = tt.Substring(6, tt.Length - 6);
            return rutax;
        }

        private static string ruta22()
        {
            string rutay = "";
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string gg = outPutDirectory + "\\ViajerosTest.txt";
            string tt = gg.Trim();
            rutay = tt.Substring(6, tt.Length - 6);
            return rutay;
        }



        private string ruta1 = ruta11();
        private string ruta2 = ruta22();



        private void setupEscenario1()
        {
            w1 = new WorldAirline();
        }

        
        private void setupEscenario2()
        {
            w2 = new WorldAirline();
        }

        private void setupEscenario3()
        {
            w3 = new WorldAirline();
            
            
        }



        [TestMethod]
        public void testFuerzaBruta()
        {
            setupEscenario1();
            w1.cargarCiudades(ruta1);
            w1.cargarViajeros(ruta2, 0, 5);
            var viajeros = w1.Viajeros.Values;
            Viajero jorge = null;
            List<Ciudad> ciudades = new List<Ciudad>();
            double distancia = 0;
           
            foreach (var item in viajeros)
            {
                Viajero tem = (Viajero)item;
                if (tem.Id.Equals("41540")){
                    jorge = tem;
                }
            }

            ciudades = jorge.Itinerario.itinerarioExploracionCompleta();

            for (int i = 0; (i+1) < ciudades.Count; i++)
            {
                Ciudad temp = ciudades[i];
               
                distancia += temp.distancia(ciudades[i+1], 'K');
               
            }
                 Ciudad laPrimera = ciudades[0];
                 Ciudad laUltima = ciudades [ciudades.Count-1];

            distancia += laUltima.distancia(laPrimera, 'K');
            Assert.AreEqual(15717.21, distancia, 0.2);
            
        }

        [TestMethod]
        public void testEficiente()
        {

            setupEscenario1();
            w1.cargarCiudades(ruta1);
            w1.cargarViajeros(ruta2, 0, 5);
            var viajeros = w1.Viajeros.Values;
            Viajero jorge = null;
            List<Ciudad> ciudades = new List<Ciudad>();
            double distancia = 0;

            foreach (var item in viajeros)
            {
                Viajero tem = (Viajero)item;
                if (tem.Id.Equals("41540"))
                {
                    jorge = tem;
                }
            }

            ciudades = jorge.Itinerario.itinerarioEficiente();

            for (int i = 0; (i + 1) < ciudades.Count; i++)
            {
                Ciudad temp = ciudades[i];

                distancia += temp.distancia(ciudades[i + 1], 'K');

            }
            Ciudad laPrimera = ciudades[0];
            Ciudad laUltima = ciudades[ciudades.Count - 1];

            distancia += laUltima.distancia(laPrimera, 'K');
            Assert.AreEqual(15717.21, distancia, 0.2);


        }




 

    





    }
}