using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarollo.Models;
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

    public async Task<List</*EmpresaEmpleadosDTO*/EmpresaEmpleadoListResponse>> GetEmpresaConEmpleados()
    {
        try
        {
            List</*EmpresaEmpleadosDTO*/EmpresaEmpleadoListResponse> response=await repository.GetEmpresasConEmpleados();
            return response;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
