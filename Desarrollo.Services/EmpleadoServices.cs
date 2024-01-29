using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarollo.Models;
using Desarrollo.Data;
using Microsoft.Extensions.Configuration;

namespace Desarrollo.Services
{
    public class EmpleadoServices
    {
        #region Fields
        private EmpleadoRepository repository;
        #endregion
        #region Constructor
        public EmpleadoServices(IConfiguration consfiguration)
        {
            this.repository=new EmpleadoRepository(consfiguration);
        }
        #endregion
        #region Public Methods
        public async Task<IEnumerable> GetEmpleados()
        {
            try
            {
                IEnumerable response=await repository.GetEmpleados();
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<EmpleadosDTO> CreateEmpleado(PostEmpleadoTDto body)
        {
            try
            {
                int id=await repository.CreateEmpleado(body);
                EmpleadosDTO response=await repository.GetEmpleadoById(id);
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<EmpleadosDTO> GetEmpleadoById(int id)
        {
            try
            {
                EmpleadosDTO response=await repository.GetEmpleadoById(id);

                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<EmpleadosDTO>> GetEmpleadoByQuery(string? query)
        {
            try
            {
                IEnumerable<EmpleadosDTO> response = await repository.GetEmpleadoByQuery(query);
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        #endregion
    }
}