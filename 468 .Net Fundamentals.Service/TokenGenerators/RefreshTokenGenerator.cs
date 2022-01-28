using _468_.Net_Fundamentals.Service.TokenValidators;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace _468_.Net_Fundamentals.Service.TokenGenerators
{
    public class RefreshTokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        public RefreshTokenGenerator(IConfiguration configuration, RefreshTokenValidator refreshTokenValidator)
        {
            _configuration = configuration;
            _refreshTokenValidator = refreshTokenValidator;
        }

        public string GenerateToken()
        {

            var authSiginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:RefreshTokenSecret"]));
            var refreshToken = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(5),
                    signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);

            /*var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }*/
        }
    }
}
