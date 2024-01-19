using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Desarollo.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Operators;

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
            this._configuration = configuration;
            this._DDBB = this._configuration.GetConnectionString("DefaultConnection")!;
        }
        #endregion
        #region Public Methods
        public async Task<IEnumerable<EmpleadosDTO>> GetEmpleados()
        {
            try
            {
                string QUERY = "SELECT ep.id as Id, ep.nombre as Nombre, ep.apellido as Apellido, es.nombre as Empresa, c.nombre_cargo as Cargo FROM Empleado ep JOIN Empresa es on ep.empresaId = es.id JOIN CargoEmpleado ce on ce.empleadoId = ep.id JOIN Cargo c on c.id = ce.cargoId";
                using MySqlConnection connection = new MySqlConnection(_DDBB);
                IEnumerable<EmpleadosDTO> empleados = await connection.QueryAsync<EmpleadosDTO>(QUERY);
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

        public async Task CreateEmpleado(PostEmpleadoTDto body)
        {
            using (MySqlConnection connection = new MySqlConnection(_DDBB))
            {
                connection.Open();
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        Empleado e=new Empleado{Nombre=body.Nombre, Apellido=body.Apellido, EmpresaId=body.EmpresaId};
                        string queryEmp="insert into Empleado (Nombre, Apellido, EmpresaId) values (@Nombre, @Apellido, @EmpresaId); SELECT LAST_INSERT_ID()";
                        int empleadoId=connection.ExecuteScalar<int>(queryEmp,e,transaction);
                        
                        CargoEmpleado ce=new CargoEmpleado{EmpleadoId=empleadoId, CargoId=body.CargoId};
                        string querCargoEmpleado="insert into CargoEmpleado (EmpleadoId, CargoId) values (@EmpleadoId, @CargoId);";
                        var result=await connection.ExecuteAsync(querCargoEmpleado,ce,transaction);

                        transaction.Commit();
                    }
                    catch (System.Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                };
            };
        }
        #endregion
    }
}