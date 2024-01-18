using Desarollo.Models;
using Desarrollo.Data;
using Microsoft.Extensions.Configuration;

namespace Desarrollo.Services;

public class ClienteServices
{
    private readonly ClienteRepository repository;

    public ClienteServices(IConfiguration configuration)
    {
        this.repository=new ClienteRepository(configuration);
    }

    public async Task<IEnumerable<Cliente>> GetClienteList()
    {
        try
        {
            IEnumerable<Cliente> response=await repository.GetClienteList();
            return response;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
