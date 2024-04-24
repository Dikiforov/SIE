using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_SIE
{
    class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public Persona(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }
    }
}
