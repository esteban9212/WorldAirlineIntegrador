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
            w1.cargarViajeros(ruta2, 0, 11);
            List<Viajero> buscado = w1.buscarViajeros("Edna");
            List<Arista<Ciudad>> completa = new List<Arista<Ciudad>>();
            Viajero edna = null;
            foreach (var item in buscado)
            {
                Viajero temp = (Viajero)item;
                if (temp.Nombre.Equals("Edna"))
                {
                    edna = temp;
                }
            }

            //completa = edna.itinerarioExploracionCompleta();

            Assert.AreEqual(4, completa.Count);
        }

        [TestMethod]
        public void testFuerzaBruta2()
        {
            setupEscenario1();
            w1.cargarCiudades(ruta1);
            w1.cargarViajeros(ruta2, 0, 11);
            List<Viajero> buscado = w1.buscarViajeros("Edna");
            List<Arista<Ciudad>> completa = new List<Arista<Ciudad>>();
            Viajero edna = null;
            double peso = 0;
            foreach (var item in buscado)
            {
                Viajero temp = (Viajero)item;
                if (temp.Nombre.Equals("Edna"))
                {
                    edna = temp;
                }
            }

            //completa = edna.itinerarioExploracionCompleta();

            foreach (var item in completa)
            {
                Arista<Ciudad> mia = (Arista<Ciudad>)item;
                peso += mia.Distancia;

            }

            Assert.AreEqual(24470, peso);


        }





    }
}