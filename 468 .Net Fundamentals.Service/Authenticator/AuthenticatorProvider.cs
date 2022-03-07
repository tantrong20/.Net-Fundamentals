﻿using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Domain.ViewModels.Authenticate;
using _468_.Net_Fundamentals.Service.TokenGenerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service.Authenticator
{
    public class AuthenticatorProvider
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticatorProvider(IUnitOfWork unitOfWork, RefreshTokenGenerator refreshTokenGenerator, AccessTokenGenerator accessTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _refreshTokenGenerator = refreshTokenGenerator;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<AuthenticatedRespone> Authenticate(AppUser user)
        {

            var authClaims = new List<Claim>
                {
                    new Claim("Id", user.Id),
                    new Claim("Name", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            string accessToken = _accessTokenGenerator.GenerateToken(authClaims);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            return new AuthenticatedRespone
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

       
    }
}
