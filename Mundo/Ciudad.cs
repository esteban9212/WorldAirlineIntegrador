using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundo
{
   public class Ciudad
    {


        /*
        * atributos de la clase
        */
        private string nombre;
        private string id;
        private double longitud;
        private double latitud;
        private int totalPoblacion;
        private int posEnGrafo;
        private int cantidadAdyacencias;


        /*
        * constructor de la clase
        */
        public Ciudad(string nom, string ident, double longi, double lat, int totalPob)
        {
            nombre = nom;
            id = ident;
            longitud = longi;
            latitud = lat;
            totalPoblacion = totalPob;
        }
        public int CantidadAdyacencias
        {
            get
            {
                return cantidadAdyacencias;
            }

            set
            {
                cantidadAdyacencias = value;
            }
        }

        /*
        * metodos de la clase
        */
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

        public double Longitud
        {
            get
            {
                return longitud;
            }

            set
            {
                longitud = value;
            }
        }

        public double Latitud
        {
            get
            {
                return latitud;
            }

            set
            {
                latitud = value;
            }
        }

        public int TotalPoblacion
        {
            get
            {
                return totalPoblacion;
            }

            set
            {
                totalPoblacion = value;
            }
        }

        public int PosEnGrafo
        {
            get
            {
                return posEnGrafo;
            }

            set
            {
                posEnGrafo = value;
            }
        }


       

       
    }
}
