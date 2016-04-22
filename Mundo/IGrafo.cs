using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundo
{
    interface IGrafo<T>
    {
        void kruskalOpcion1();
        List<Arista<T>> kruskalOpcion2();
        double[,] calcularDistancias();
    }
}
