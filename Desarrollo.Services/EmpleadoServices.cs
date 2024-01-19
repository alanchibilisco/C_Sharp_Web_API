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
        #endregion
    }
}