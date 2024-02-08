using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarrollo.ContextDB;
using Desarrollo.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Desarrollo.Data
{
    public class UserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context=context;
        }

        public async Task<bool> UserExist(string email)
        {
            try
            {
                var user=await _context.User.Where(user=>user.Email==email).FirstOrDefaultAsync();

               /* var test=await _context.User.FromSqlRaw("select * from User").ToListAsync();
                System.Console.WriteLine($"TEST--> {JsonSerializer.Serialize(test)}");*/
                System.Console.WriteLine($"USER--> {JsonSerializer.Serialize(user)}");
                if(user!=null)
                {
                    return true;
                }else
                {
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
                var result=await _context.User.Where(user=> user.Email==email).FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

    }
}