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

        public double distancia(Ciudad laOtra, char unit)
        {
            double theta = this.Longitud - laOtra.Longitud;
            double dist = Math.Sin(degArad(this.Longitud)) * Math.Sin(degArad(laOtra.Longitud)) + Math.Cos(degArad(this.Longitud)) * Math.Cos(degArad(laOtra.longitud)) * Math.Cos(degArad(theta));
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



    }
}
