using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarollo.Models;
using Desarrollo.Data;
using Desarrollo.Services.helpers;
using Microsoft.Extensions.Configuration;

namespace Desarrollo.Services
{
    public class UserService
    {
        private UserRepository repository;

        public UserService(IConfiguration configuration)
        {
            this.repository=new UserRepository(configuration);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                IEnumerable<User> response = await repository.GetAll();
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<User> CreateNewUser(PostUserDto body)
        {
            try
            {
                bool result=await repository.UserExists(body.Email);
                if (result)
                {
                    throw new Exception("Usuario ya registrado");
                }
                string hashPassword=Security.CreateHash(body.Password, Security.GetSalt());
                int newUserId=await repository.CreateNewUser(body.Email, hashPassword, body.Role);
                User response=await repository.GetUserById(newUserId);
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<User?> Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email)||string.IsNullOrEmpty(password))
                {
                    throw new Exception("Usuario y/o password son requeridos");
                }

                User? response=await repository.GetUserByEmail(email);
                
                if (response==null)
                {
                    throw new Exception("Usuario y/o contraseña incorrectos");
                }
                
                if (!Security.ValidateHash(password, Security.GetSalt(), response.Password))
                {
                    throw new Exception("Usuario y/o contraseña incorrectos");
                }
                
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}