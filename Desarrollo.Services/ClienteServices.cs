using System;
using System.Collections;
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

        public ClienteServices(TestContext context)
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

        public async Task<IEnumerable> GetAllClientes()
        {
            try
            {
                IEnumerable response=await repository.GetAllClientes();
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
                ClienteResponse response=await repository.GetClienteByEmail(email);
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}