using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desarrollo.Models
{
    public class CargoEmpleado
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public int CargoId { get; set; }
    }
}