using System.Collections;
using Desarrollo.ContextDB;
using Desarrollo.Data;

namespace Desarrollo.Services;

public class CargoServices
{
    private readonly CargoRepository repository;

    public CargoServices(Context context)
    {
        this.repository=new CargoRepository(context);
    }

    public async Task<IEnumerable> GetCargosTodos()
    {
        try
        {
            var response=await repository.Cargos();
            return response;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
