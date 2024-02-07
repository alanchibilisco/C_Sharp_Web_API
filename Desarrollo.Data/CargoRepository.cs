namespace Desarrollo.Data;

using System.Collections;
using Desarrollo.ContextDB;
using Desarrollo.Modelos;
using Microsoft.EntityFrameworkCore;

public class CargoRepository
{
    private readonly Context _context;

    public CargoRepository(Context context)
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
}
