//using Desarrollo.Data;

//using Desarrollo.Models;
using Desarrollo.ContextDB;
using Desarrollo.Modelos;


namespace Desarrollo.Repository
{
    public class ClienteRepository
    {
        private readonly Context _context;

        public ClienteRepository(Context context)
        {
            _context=context;
        }


        public async Task<Cliente> CreateNewCliente(PostClienteDto2 body)
        {
            try
            {
                Cliente c=new Cliente{Nombre=body.Nombre, Email=body.Email,EmpresaId=body.EmpresaId};
                _context.Cliente.Add(c);
                var result=await _context.SaveChangesAsync();
                return c;
            }
            catch (System.Exception ex)
            {                
                System.Console.WriteLine("Error--> {0}",ex);
                throw;
            }
        }
    }
}