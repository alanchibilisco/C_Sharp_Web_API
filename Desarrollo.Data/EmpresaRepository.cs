using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using MySql.Data.MySqlClient;
using Desarollo.Models;
using System.Collections;
using System.Linq;
using System.Net.Http.Headers;

namespace Desarrollo.Data
{
    public class EmpresaRepository
    {
        #region Fields
        private readonly IConfiguration _configuration;
        #endregion
        #region Constructor
        public EmpresaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable
        > GetEmpresasConClientes()
        {
            try
            {
                string query = @"SELECT * FROM Empresa e LEFT JOIN Cliente c on e.id = c.empresaId ORDER BY e.id, c.id;";
                using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                //var response = await connection.QueryAsync(query);
                var empresaConClientes= new Dictionary<int, Empresa>();
                var response=await connection.QueryAsync<Empresa,Cliente,Empresa>(query,(empresa,cliente)=>{
                    if (empresaConClientes.TryGetValue(empresa.Id, out var empresaActual))
                    {
                        empresaActual=empresa;
                        empresaActual.clientes=new List<Cliente>();
                        empresaConClientes.Add(empresa.Id, empresaActual);
                    }
                    if(cliente!=null){
                        empresaActual.clientes.Add(cliente);
                    }
                   //empresa.clientes??=new List<Cliente>();
                   //System.Console.WriteLine("Cliente--> {0}-{1}-{2}",cliente.Id, cliente.Email, cliente.Nombre);
                   //System.Console.WriteLine("empresa--> {0}-{1}-{2}",empresa.Id, cliente.Email, cliente.EmpresaId);
                   //empresa.clientes.Add(cliente);
                   return empresaActual;
                }, splitOn:"id");
                /*var empresaConClientes= new Dictionary<int, Empresa>();
                var response= await connection.QueryAsync<Empresa, Cliente, Empresa>(query,(empresa, cliente)=>{
                    if (!empresaConClientes.TryGetValue(empresa.Id, out var empresaActual))
                    {
                        empresaActual=empresa;
                        empresaActual.clientes=new List<Cliente>();
                        empresaConClientes.Add(empresa.Id, empresaActual);
                    }
                    if (cliente!=null)
                    {
                    empresaActual.clientes.Add(cliente);    
                    }
                    
                    return empresaActual;
                }, splitOn:"clienteId");*/
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