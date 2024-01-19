using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Desarollo.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Desarrollo.Data
{
    public class EmpleadoRepository
    {
        #region Fields
        private readonly IConfiguration _configuration;
        private readonly string _DDBB;
        #endregion
        #region Constructor
        public EmpleadoRepository(IConfiguration configuration)
        {
            this._configuration=configuration;
            this._DDBB=this._configuration.GetConnectionString("DefaultConnection")!;
        }
        #endregion
        #region Public Methods
        public async Task<IEnumerable<EmpleadosDTO>> GetEmpleados()
        {
            try
            {
                 string QUERY="SELECT ep.id as Id, ep.nombre as Nombre, ep.apellido as Apellido, es.nombre as Empresa, c.nombre_cargo as Cargo FROM Empleado ep JOIN Empresa es on ep.empresaId = es.id JOIN CargoEmpleado ce on ce.empleadoId = ep.id JOIN Cargo c on c.id = ce.cargoId";
                 using MySqlConnection connection=new MySqlConnection(_DDBB);
                 IEnumerable<EmpleadosDTO> empleados=await connection.QueryAsync<EmpleadosDTO>(QUERY);
                 /*List<EmpleadosDTO>response=new List<EmpleadosDTO>();
                 foreach (var item in empleados)
                 {
                    response.Add(new EmpleadosDTO{Id=item.Id, Nombre=item.Nombre, Apellido=item.Apellido, Empresa=item.Empresa, Cargo=item.Cargo});
                 }
                 return response;*/
                 return empleados;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        #endregion
    }
}