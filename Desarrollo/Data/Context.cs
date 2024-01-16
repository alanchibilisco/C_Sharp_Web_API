
using Desarrollo.Models;
using Microsoft.EntityFrameworkCore;

namespace Desarrollo.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {
            
        }

        public DbSet<Empresa> Empresa {get;set;}
    }
}