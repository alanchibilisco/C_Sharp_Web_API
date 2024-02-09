namespace Desarrollo.Data;

using System.Collections;
using Desarrollo.ContextDB;
using Desarrollo.Modelos;
using Microsoft.EntityFrameworkCore;

public class CargoRepository
{
    private readonly TestContext _context;

    public CargoRepository(TestContext context)
    {
        this._context=context;
    }

    public async Task<IEnumerable> Cargos()
    {
        try
        {
            List<Cargo> cargos=await _context.Cargo.ToListAsync();
            return cargos;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<Cargo> CreateNewCargo(PostCargoDto body)
    {
        try
        {
            Cargo c=new Cargo{Nombre_Cargo=body.Nombre_Cargo};
            _context.Cargo.Add(c);
            await _context.SaveChangesAsync();
            return c;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
