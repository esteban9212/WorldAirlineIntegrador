using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mundo;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace AerolineaTest
{
    [TestClass]
    public class TestWorldAirline
    {

        //Estructuras
        private string[] arreglo = { "bombay", "shanghai", "tokyo" };
        //Referencias
        private WorldAirline w1;
        private WorldAirline w2;
        private WorldAirline w3;

        //Rutas
        private string ruta1 = @"G:CiudadesText.txt";
        private string ruta2 = @"G:Viajeros.txt";
        private string ruta3 = @"G:NuevasCiudades.txt";

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
            setupEscenario2();
            //Primeros usuarios cargados: ana,aurore,britno,celine,annalee,benedic
            //cammy,chelsie,asha,branda,cassondra,arianne,billy,carol,chu,annete

            List<Viajero> nueva = w2.buscarViajeros("Ana");
            string nombre = "";
           for (int i = 0; i < nueva.Count; i++)
            {
                if (nueva[i].Nombre.Equals("Ana"))
                {
                    nombre = "Ana";
               }
           }

          Assert.AreEqual("Ana", nombre);


        }


        [TestMethod]
        public void cargarCiudadesTest1()
        {
            setupEscenario3();
            w3.cargarCiudades(ruta1);
            int cantPaises = w3.Paises.Count;
            Assert.AreEqual(19, cantPaises);
        }

        [TestMethod]
        public void cargarCiudesTest2()
        {
            setupEscenario3();
            w3.cargarCiudades(ruta1);
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
            w3.cargarCiudades(ruta3);

          //  List<Ciudad> filtradas = w3.filtrarCiudades(12000000);
          Hashtable filtradas = w3.filtrarCiudades(12000000);

              Assert.AreEqual(3, filtradas.Count);
        }

        [TestMethod]
        public void filtrarCiudadesTest2()
        {
            setupEscenario3();
            w3.cargarCiudades(ruta3);
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