using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_SIE
{
    class Coche
    {
        public string marca {  get; set; }
        public string modelo { get; set; }
        public string VIN { get; set; }

        public Coche(string marca, string modelo, string vIN)
        {
            this.marca = marca;
            this.modelo = modelo;
            VIN = vIN;
        }
    }
}
