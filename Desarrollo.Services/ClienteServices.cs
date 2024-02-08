using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarrollo.ContextDB;
using Desarrollo.Data;
using Desarrollo.Modelos;

namespace Desarrollo.Services
{
    public class ClienteServices
    {
        private ClienteRepository repository;

        public ClienteServices(Context context)
        {
            repository=new ClienteRepository(context);
        }

        public async Task<Cliente> CreateNewCliente(PostClienteDto2 body)
        {
            try
            {
                Cliente response=await repository.CreateNewCliente(body);
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}