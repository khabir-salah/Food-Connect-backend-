using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Application.Queries.UserLogin;

namespace Application.Queries
{
    public class Authentication
    {

        public class TokenClaims
        {
            public string Id { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Role { get; set; } = null!;
        }

        public static string  GenerateJWTAuthetication(LoginResponseModel request)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, request.UserId.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, request.RoleName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"])));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Convert.ToString(ConfigurationManager.AppSettings["config:JwtIssuer"]),
                Convert.ToString(ConfigurationManager.AppSettings["config:JwtAudience"]),
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static TokenClaims  ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"]));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true, 
                    ValidateAudience = true, 
                    ValidIssuer = Convert.ToString(ConfigurationManager.AppSettings["config:JwtIssuer"]),
                    ValidAudience = Convert.ToString(ConfigurationManager.AppSettings["config:JwtAudience"]),
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                // Corrected access to the validatedToken
                var jwtToken = (JwtSecurityToken)validatedToken;
                var id = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                var email = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
                var role = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;

                return new TokenClaims
                {
                    Email = email,
                    Role = role,    
                    Id = id,
                };
            }
            catch
            {
                return null;
            }
        }

    }
}
