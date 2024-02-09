using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarollo.Models;

namespace Desarrollo.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> CreateNewUser(PostUserDto body);
        Task<LoginResponseDto> Login(string email, string password);
        
    }
}