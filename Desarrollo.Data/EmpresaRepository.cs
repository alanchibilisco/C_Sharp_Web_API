using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using MySql.Data.MySqlClient;
using Desarollo.Models;
using System.Collections;

namespace Desarrollo.Data
{
    public class EmpresaRepository
    {
        #region Fields
        private readonly IConfiguration _configuration;
        private readonly string _DDBB;
        #endregion
        #region Constructor
        public EmpresaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _DDBB=_configuration.GetConnectionString("DefaultConnection")!;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable> GetEmpresasConClientes()
        {
            try
            {
                string query = @"SELECT * FROM Empresa e LEFT JOIN Cliente c on e.id = c.empresaId ORDER BY e.id, c.id;";
                using MySqlConnection connection = new MySqlConnection(_DDBB);
                
                Dictionary<int,EmpresaClientesDTO> empresaConClientes = new Dictionary<int, EmpresaClientesDTO>();

                await connection.QueryAsync<EmpresaClientesDTO, Cliente, EmpresaClientesDTO>(query, (empresa, cliente) =>
                {
                    if (!empresaConClientes.TryGetValue(empresa.Id, out var empresaActual))
                    {
                        empresaActual = empresa;
                        empresaActual.Clientes = new List<Cliente>();
                        empresaConClientes.Add(empresa.Id, empresaActual);
                    }
                    if (cliente != null)
                    {
                        empresaActual.Clientes.Add(cliente);
                    }

                    return empresaActual;
                }, splitOn: "id");
              
                List<EmpresaClientesDTO> response = empresaConClientes.Values.ToList();
                //return response;
                return response;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<List</*EmpresaEmpleadosDTO*/EmpresaEmpleadoListResponse>> GetEmpresasConEmpleados()
        {
            try
            {
                /* V1:
                string query="select * from Empresa e left join Empleado em on em.empresaId = e.id left join CargoEmpleado ce on ce.empleadoId = em.id left join Cargo c on c.id = ce.cargoId order by e.id";

                using MySqlConnection connection=new MySqlConnection(_DDBB);
                Dictionary<int, EmpresaEmpleadosDTO> response=new Dictionary<int, EmpresaEmpleadosDTO>();

                await connection.QueryAsync<EmpresaEmpleadosDTO, Empleado, EmpresaEmpleadosDTO>(query,(empresa, empleado)=>{
                    if (!response.TryGetValue(empresa.Id, out var empresaActual))
                    {
                        empresaActual=empresa;
                        empresaActual.Empleados=new List<Empleado>();
                        response.Add(empresa.Id, empresaActual);
                    };
                    if (empleado!=null)
                    {
                        empresaActual.Empleados.Add(empleado);
                    }
                    return empresaActual;
                }, splitOn:"id");
                List<EmpresaEmpleadosDTO> finalResponse=response.Values.ToList();
                return finalResponse;*/
                string query ="select e.id, e.nombre, em.id as empleadoId, em.nombre as empleadoNombre, em.apellido as empleadoApellido, c.nombre_cargo as empleadoCargo from Empresa e left join Empleado em on em.empresaId = e.id left join CargoEmpleado ce on ce.empleadoId = em.id left join Cargo c on c.id = ce.cargoId order by e.id;";

                using MySqlConnection connection=new MySqlConnection(_DDBB);

                IEnumerable<EmpresaEmpleadoQueryResponse> response=await connection.QueryAsync<EmpresaEmpleadoQueryResponse>(query);

                Dictionary<int,EmpresaEmpleadoListResponse> dic=new Dictionary<int, EmpresaEmpleadoListResponse>();

                foreach (EmpresaEmpleadoQueryResponse item in response)
                {
                    if (dic.TryGetValue(item.id, out EmpresaEmpleadoListResponse value))
                    {
                        if (item.empleadoId!=null)
                        {
                            value.Empleados.Add(new EmpleadoItem{empleadoId=item.empleadoId, empleadoNombre=item.empleadoNombre, empleadoApellido=item.empleadoApellido, empleadoCargo=item.empleadoCargo});
                        }
                        
                    }else{
                        List<EmpleadoItem> list=new List<EmpleadoItem>();
                         EmpresaEmpleadoListResponse eel=new EmpresaEmpleadoListResponse{id=item.id,nombre=item.nombre};
                        
                        if (item.empleadoId!=null)
                        {
                            EmpleadoItem ei=new EmpleadoItem{empleadoId=item.empleadoId, empleadoNombre=item.empleadoNombre, empleadoApellido=item.empleadoApellido, empleadoCargo=item.empleadoCargo};
                             list.Add(ei);
                            eel.Empleados=list;
                        }else{
                            eel.Empleados=null;
                        }                       
                       
                       
                        dic.Add(item.id, eel);
                    }
                }
                List<EmpresaEmpleadoListResponse> FinalResponse=dic.Values.ToList();
                return FinalResponse;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        #endregion
    }
}