﻿using Application.Features.Interfaces.IServices;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Queries.Get.UserLogin;

namespace Application.Features.Queries.GeneralServices
{
    public class Authentication : IAuthentication
    {
        //private readonly IConfiguration _configuration;
        //public Authentication(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public class JWT
        {
            public string Key { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public double DurationInMinutes { get; set; }
        }

        //public class TokenClaims
        //{
        //    public string Id { get; set; } = null!;
        //    public string Email { get; set; } = null!;
        //    public string Role { get; set; } = null!;
        //}


        private readonly JWT _jwt;
        public Authentication(IOptions<JWT> jwt)
        {
            _jwt = jwt.Value;
        }
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Roles", user.Role.Name),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        //public  string GenerateJWTAuthetication(string email, Guid role, Guid Id)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
        //        new Claim(ClaimTypes.Email, email),
        //        new Claim(ClaimTypes.Role, role.ToString())
        //    };

        //    var key = new SymmetricSecurityKey(
        //     Encoding.UTF8.GetBytes(_configuration["config:JwtKey"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        _configuration["config:JwtIssuer"],
        //        _configuration["config:JwtAudience"],
        //        claims,
        //        expires: DateTime.Now.AddMinutes(15),
        //        signingCredentials: creds
        //    );
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}




        //public static TokenClaims ValidateToken(string token)
        //{
        //    if (token == null)
        //        return null;

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"]));
        //    try
        //    {
        //        tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidIssuer = Convert.ToString(ConfigurationManager.AppSettings["config:JwtIssuer"]),
        //            ValidAudience = Convert.ToString(ConfigurationManager.AppSettings["config:JwtAudience"]),
        //            ClockSkew = TimeSpan.Zero

        //        }, out SecurityToken validatedToken);

        //        // Corrected access to the validatedToken
        //        var jwtToken = (JwtSecurityToken)validatedToken;
        //        var id = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
        //        var email = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
        //        var role = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;

        //        return new TokenClaims
        //        {
        //            Email = email,
        //            Role = role,
        //            Id = id,
        //        };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

    }
}
