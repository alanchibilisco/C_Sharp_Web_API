using System.Collections;
using Desarrollo.ContextDB;
using Desarrollo.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Desarrollo.Data;

public class ClienteRepository
{
    private readonly TestContext _context;

    public ClienteRepository(TestContext context)
    {
        this._context=context;
    }

    public async Task<Cliente> CreateNewCliente(PostClienteDto2 body){
        try
        {
            Cliente c=new Cliente{Nombre=body.Nombre, Email=body.Email, EmpresaId=body.EmpresaId};
            _context.Cliente.Add(c);
            int result =await _context.SaveChangesAsync();
            return c;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<IEnumerable> GetAllClientes()
    {
        try
        {
            
            IEnumerable response =await  _context.Cliente.Select(cliente=> new{
                id=cliente.Id,
                nombre=cliente.Nombre,
                email=cliente.Email,
                Empresa=_context.Empresa.Where(empresa=>empresa.Id == cliente.EmpresaId).Select(empresa=>empresa.Nombre).First()
            }).ToListAsync();
            return response;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<ClienteResponse> GetClienteByEmail(string email)
    {
        try
        {
            ClienteResponse response=await _context.Cliente.Select(cliente=>new ClienteResponse{
                Id=cliente.Id,
                Nombre=cliente.Nombre,
                Email=cliente.Email,
                Empresa=_context.Empresa.Where(empresa=> empresa.Id==cliente.EmpresaId).Select(empresa=>empresa.Nombre).First()
            }).Where(cliente=> cliente.Email==email).FirstAsync();
            return response;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
