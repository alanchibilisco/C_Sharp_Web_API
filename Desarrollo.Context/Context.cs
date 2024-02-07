namespace Desarrollo.ContextDB;

using Microsoft.EntityFrameworkCore;
using Desarrollo.Modelos;


public class Context:DbContext
{
    public Context(DbContextOptions<Context> options):base(options)
    {
        
    }

    public DbSet<Cargo> Cargo {get;set;}
}
