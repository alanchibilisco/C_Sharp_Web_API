using System.Collections;
using Dapper;
using Desarollo.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace Desarrollo.Data;

public class ClienteRepository
{
    #region Fields
    private readonly IConfiguration _configuration;
    #endregion
    #region Constructor
    public ClienteRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    #endregion
    #region Public Methods
    public async Task<IEnumerable<Cliente>> GetClienteList()
    {
        try
        {

            string sentence = "SELECT * FROM Cliente";
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var response = await connection.QueryAsync<Cliente>(sentence);

            return response;

        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Repository Error--> {0}", ex);
            throw;
        }
    }

    public async Task NewCliente(PostClienteDto2 body)
    {
        try
        {
            Cliente c = new Cliente { Nombre = body.Nombre, Email = body.Email, EmpresaId = body.EmpresaId };
            string sql = "INSERT INTO Cliente (Nombre, Email, EmpresaId) VALUES (@Nombre, @Email, @EmpresaId)";
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var result = await connection.ExecuteAsync(sql, c);
            System.Console.WriteLine("Resultado del insert --> {0}",result);
            /*string sql2 = $"Select * FROM Cliente WHERE email = @email";
            var response = await connection.QuerySingleAsync<Cliente>(sql2, new{c.Email});
            return response;*/

        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public async Task<Cliente> GetClienteByEmail(string email)
    {
        try
        {
            string sql = $"SELECT * FROM Cliente WHERE email = @email";
            var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            Cliente response = await connection.QuerySingleAsync<Cliente>(sql, new { @email });
            return response;
        }
        catch (System.Exception)
        {

            throw;
        }
    }    
    #endregion
}
