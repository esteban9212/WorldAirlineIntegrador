using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundo
{
    public class Arista<T> 
    {
        private T destino1, destino2;
        private double distancia;


        public Arista(T dest1, T dest2, double dist)
        {
            destino1 = dest1;
            destino2 = dest2;
            distancia = dist;
        }

        public T Destino1
        {
            get
            {
                return destino1;
            }

            set
            {
                destino1 = value;
            }
        }

        public T Destino2
        {
            get
            {
                return destino2;
            }

            set
            {
                destino2 = value;
            }
        }

        public double Distancia
        {
            get
            {
                return distancia;
            }

            set
            {
                distancia = value;
            }
        }

      
    }
}
