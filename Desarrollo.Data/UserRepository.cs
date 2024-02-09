using Dapper;
using Desarollo.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Desarrollo.Data
{
    public class UserRepository:IUserRepository
    {
        private readonly string _DDBB;

        public UserRepository(IConfiguration configuration)
        {
            this._DDBB=configuration.GetConnectionString("DefaultConnection")!;
            //this._DDBB=Database.GetDataBase();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                string query="select * from User;";
                using MySqlConnection connection=new MySqlConnection(_DDBB);
                IEnumerable<User> response=await connection.QueryAsync<User>(query);
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }


        public async Task<int> CreateNewUser(string email, string password, string role)
        {
            try
            {
                using MySqlConnection connection=new MySqlConnection(_DDBB);
                string query=@"insert into User (email, password, role) values (@email, @password, @role); SELECT LAST_INSERT_ID()";
                int newUserId=await connection.ExecuteScalarAsync<int>(query, new{@email, @password, @role});
                return newUserId;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                using MySqlConnection connection=new MySqlConnection(_DDBB);
                string query=@"select * from User u where u.id = @id";
                User response=await connection.QueryFirstAsync<User>(query, new{@id});
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<bool> UserExists(string email)
        {
            try
            {
                using MySqlConnection connection=new MySqlConnection(_DDBB);
                string query=@"select * from User u where u.email = @email";
                var response=await connection.QuerySingleOrDefaultAsync<User>(query, new{@email});
                if (response!=null)
                {
                    return true;
                }else{
                    return false;
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            try
            {
                using MySqlConnection connection=new MySqlConnection(_DDBB);
                string query=@"select * from User u where u.email = @email;";
                User response=await connection.QuerySingleOrDefaultAsync<User>(query,new{@email});
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}