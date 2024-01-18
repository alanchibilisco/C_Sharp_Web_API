using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarrollo.Data;
using Microsoft.Extensions.Configuration;
namespace Desarrollo.Services;

public class EmpresaServices
{
    private EmpresaRepository repository;

    public EmpresaServices(IConfiguration configuration)
    {
        repository = new EmpresaRepository(configuration);
    }


    public async Task<IEnumerable> GetEmpresasConCLientes()
    {
        try
        {
            var response = await repository.GetEmpresasConClientes();
            return response;
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
