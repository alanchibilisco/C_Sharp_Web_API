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
        #endregion
    }
}