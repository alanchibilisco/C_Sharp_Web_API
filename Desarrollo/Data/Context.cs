
using Desarrollo.Models;
using Microsoft.EntityFrameworkCore;

namespace Desarrollo.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<CargoEmpleado> CargoEmpleado { get; set; }
        public DbSet<Cliente> Cliente{get;set;}
    }
}