using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desarollo.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace Desarrollo.Services.helpers
{
    public class Security
    {
        private static string SecretKey { get; }="SECRET_KEY_TOKEN_TEST_FOR_LEARNING";

        private static string IssuerToken{get;}="http://localhost:5174";

        private static string AudienceToken{get;}="ApiRest";


        public static string CreateHash(string value, string salt)
        {
            var valueBytes=KeyDerivation.Pbkdf2(password:value, salt:Encoding.UTF8.GetBytes(salt), prf:KeyDerivationPrf.HMACSHA512, iterationCount:10000, numBytesRequested: 256/8);
            return Convert.ToBase64String(valueBytes);
        }

        public static bool ValidateHash(string value, string salt, string hash)
        {
            return CreateHash(value,salt)==hash;
        }

        public static string GetSalt()=>"Test";

        public static string GenerateJWT(User user)
        {
            List<Claim> claims=new List<Claim>{
                new Claim("role", user.Role),                
            };
            JwtSecurityToken token= new JwtSecurityToken(Security.IssuerToken,Security.AudienceToken, claims, expires:DateTime.Now.AddDays(1), signingCredentials:new SigningCredentials(
				new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(Security.SecretKey)
				),
				SecurityAlgorithms.HmacSha256
			));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}