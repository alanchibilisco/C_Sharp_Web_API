namespace Desarrollo.ContextDB;

using Microsoft.EntityFrameworkCore;
using Desarrollo.Modelos;


public class TestContext:DbContext
{
    public TestContext(DbContextOptions<TestContext> options):base(options)
    {
        
    }

    public DbSet<Cargo> Cargo {get;set;}

    public DbSet<CargoEmpleado> CargoEmpleado {get;set;}

    public DbSet<Cliente> Cliente {get;set;}

    public DbSet<Empresa> Empresa {get;set;}

    public DbSet<Empleado> Empleado {get;set;}

    public DbSet<User> User{get;set;}
    
}
