using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundo
{
    public class Pais
    {
        private string codigo;
        private Hashtable ciudades;


        public Pais(string nCodigo, Hashtable lasCiudades)
        {
            codigo = nCodigo;
            ciudades = lasCiudades;
        }

        public string Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
            }
        }

        public Hashtable Ciudades
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

       

    }
}
