using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desarrollo.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }=string.Empty;
        public string Apellido { get; set; }=string.Empty;

        public int EmpresaId{get;set;}
    }
}