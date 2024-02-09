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
    public class UserService:IUserService
    {
        private IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository=repository;
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

        public async Task<LoginResponseDto> Login(string email, string password)
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
                string Token=Security.GenerateJWT(response);
                LoginResponseDto FinalResponse=new LoginResponseDto{Email=response.Email, Token=Token};
                return FinalResponse;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}