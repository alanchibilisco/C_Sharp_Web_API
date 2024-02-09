using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarrollo.Modelos;

namespace Desarrollo.Data
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);
        Task<User?> GetUserByEmail(string email);

    }
}