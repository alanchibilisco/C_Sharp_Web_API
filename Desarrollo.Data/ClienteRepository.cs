using System.Collections;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Desarrollo.Data;

public class ClienteRepository
{
    private readonly IConfiguration _configuration;

    public ClienteRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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
}
