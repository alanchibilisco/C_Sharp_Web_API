using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarrollo.Modelos;

namespace Desarrollo.Services
{
    public interface IUserService
    {
        Task<bool> UserExists(string email);
        Task<User?> GetUserByEmail(string email);

    }
}