using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _468_.Net_Fundamentals.Controllers.TokenValidator
{
    [Route("api/token")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokensController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /*     public IActionResult Refresh(string token, string refreshToken)
             {
                 var principal = GetPrincipalFromExpiredToken(token);
                 var username = principal.Identity.Name;
                 var savedRefreshToken = GetRefreshToken(username); //retrieve the refresh token from a data store
                 if (savedRefreshToken != refreshToken)
                     throw new SecurityTokenException("Invalid refresh token");

                 var newJwtToken = GenerateToken(principal.Claims);
                 var newRefreshToken = GenerateRefreshToken();
                 DeleteRefreshToken(username, refreshToken);
                 SaveRefreshToken(username, newRefreshToken);

                 return new ObjectResult(new
                 {
                     token = newJwtToken,
                     refreshToken = newRefreshToken
                 });
             }*/

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }


    }
}
