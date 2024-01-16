using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desarrollo.Models
{
    public class Empleado
    {
        public int id { get; set; }
        public string nombre { get; set; }=string.Empty;
        public string apellido { get; set; }=string.Empty;
        public int empresaId { get; set; }
    }
}