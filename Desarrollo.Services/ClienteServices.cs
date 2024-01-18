using Desarollo.Models;
using Desarrollo.Data;
using Microsoft.Extensions.Configuration;

namespace Desarrollo.Services;

public class ClienteServices
{
    #region Fields
    private readonly ClienteRepository repository;
    #endregion
    #region Constructor 
    public ClienteServices(IConfiguration configuration)
    {
        this.repository = new ClienteRepository(configuration);
    }
    #endregion

    #region Public Methods
    public async Task<IEnumerable<Cliente>> GetClienteList()
    {
        try
        {
            IEnumerable<Cliente> response = await repository.GetClienteList();
            return response;
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public async Task<Cliente> NewCliente(PostClienteDto2 body)
    {
        try
        {
            Cliente response=await repository.NewCliente(body);
            return response;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<Cliente> GetClienteByEmail(string email)
    {
        try
        {
            Cliente response=await repository.GetClienteByEmail(email);
            return response;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    #endregion

}
