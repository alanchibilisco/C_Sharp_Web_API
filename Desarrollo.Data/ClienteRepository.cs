using Desarrollo.ContextDB;
using Desarrollo.Modelos;

namespace Desarrollo.Data;

public class ClienteRepository
{
    private readonly Context _context;

    public ClienteRepository(Context context)
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
}
