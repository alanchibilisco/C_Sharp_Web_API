using System.Collections;
using Desarrollo.ContextDB;
using Desarrollo.Data;
using Desarrollo.Modelos;

namespace Desarrollo.Services;

public class CargoServices
{
    private readonly CargoRepository repository;

    public CargoServices(TestContext context)
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

    public async Task<Cargo> CreateNewCargo(PostCargoDto body)
    {
        try
        {
            Cargo response=await repository.CreateNewCargo(body);
            return response;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
