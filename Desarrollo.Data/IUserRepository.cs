using Desarollo.Models;

namespace Desarrollo.Data;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<int> CreateNewUser(string email, string password, string role);
    Task<User> GetUserById(int id);
    Task<bool> UserExists(string email);
    Task<User?> GetUserByEmail(string email);
}
