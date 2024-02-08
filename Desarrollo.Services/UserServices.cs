using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarrollo.ContextDB;
using Desarrollo.Data;
using Desarrollo.Modelos;


namespace Desarrollo.Services
{
    public class UserServices
    {
        private readonly UserRepository _repository;

        public UserServices(Context context)
        {
            _repository=new UserRepository(context);
        }

        public async Task<bool> UserExists(string email)
        {
            try
            {
                bool exist=await _repository.UserExist(email);
                return exist;
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
                var response=await _repository.GetUserByEmail(email);
                return response;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}