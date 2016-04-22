using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mundo;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace AerolineaTest
{
    [TestClass]
    public class TestWorldAirline
    {

        //Estructuras

        private string[] arreglo = { "tokyo", "bombay,shanghai" };
        //Referencias
        private WorldAirline w1;
        private WorldAirline w2;
        private WorldAirline w3;


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


        private static string ruta33()
        {
            string rutaz = "";
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string gg = outPutDirectory + "\\CiudadesTest.txt";
            string tt = gg.Trim();
            rutaz = tt.Substring(6, tt.Length - 6);
            return rutaz;
        }

        //Rutas
        string ruta1 = ruta11();
        string ruta2 = ruta22();
        string ruta3 = ruta33();




        private void setupEscenario1()
        {
            w1 = new WorldAirline();
            w1.cargarCiudades(ruta1);
        }

        private void setupEscenario2()
        {
            w2 = new WorldAirline();
            w2.cargarCiudades(ruta1);
            w2.cargarViajeros(ruta2, 0, 49);

        }

        private void setupEscenario3()
        {
            w3 = new WorldAirline();
        }
        [TestMethod]
        public void cargarViajerosTest1()
        {
            List<Viajero> nueva = new List<Viajero>();
            setupEscenario1();
            w1.cargarViajeros(ruta2, 0, 49);
            var viajeros = w1.Viajeros.Values;


            foreach (var c in viajeros)
            {
                Viajero mio = (Viajero)c;
                if (!nueva.Contains(mio))
                {
                    nueva.Add(mio);
                }
            }

            int totalViajeros = nueva.Count;



            Assert.AreEqual(50, totalViajeros);
        }



        [TestMethod]
        public void cargarViajerosTest2()
        {
            List<Viajero> nueva = new List<Viajero>();
            setupEscenario1();
            w1.cargarViajeros(ruta2, 50, 99);
            var viajeros = w1.Viajeros.Values;


            foreach (var c in viajeros)
            {
                Viajero mio = (Viajero)c;
                if (!nueva.Contains(mio))
                {
                    nueva.Add(mio);
                }
            }

            int totalViajeros = nueva.Count;



            Assert.AreEqual(50, totalViajeros);

        }

        [TestMethod]
        public void buscarViajerosTest1()
        {
            setupEscenario3();
            string nombre = "";
            w3.cargarCiudades(ruta1);
            w3.cargarViajeros(ruta2, 0, 99);


            List<Viajero> nueva = w3.buscarViajeros("Ana");

            for (int i = 0; i < nueva.Count; i++)
            {
                nombre += nueva[i];

            }


            Assert.AreEqual("Ana", nombre);


        }


        [TestMethod]
        public void cargarCiudadesTest1()
        {
            setupEscenario3();
            w3.cargarCiudades(ruta3);
            int cantPaises = w3.Paises.Count;
            Assert.AreEqual(19, cantPaises);
        }

        [TestMethod]
        public void cargarCiudesTest2()
        {
            setupEscenario3();
            w3.cargarCiudades(ruta3);
            Hashtable paises = w3.Paises;
            var losPaises = paises.Values;
            int contador = 0;
            foreach (var item in losPaises)
            {
                Pais P = (Pais)item;
                contador += P.Ciudades.Count;
            }

            Assert.AreEqual(65, contador);
        }


        [TestMethod]
        public void filtrarCiudadesTest()
        {
            setupEscenario3();
            w3.cargarCiudades(ruta1);

            //  List<Ciudad> filtradas = w3.filtrarCiudades(12000000);
            Hashtable filtradas = w3.filtrarCiudades(12000000);

            Assert.AreEqual(3, filtradas.Count);
        }

        [TestMethod]
        public void filtrarCiudadesTest2()
        {
            setupEscenario3();
            w3.cargarCiudades(ruta1);
            Hashtable filtradas = w3.filtrarCiudades(12000000);
            var filtrados = filtradas.Values;
            string cadena1 = "";
            string cadena2 = "";
            foreach (var item in filtrados)
            {
                Ciudad c = (Ciudad)item;
                cadena1 += c.Nombre + ",";
            }

            for (int i = 0; i < arreglo.Length; i++)
            {
                cadena2 += arreglo[i] + ",";
            }

            Assert.AreEqual(cadena1, cadena2);

        }

    }
}