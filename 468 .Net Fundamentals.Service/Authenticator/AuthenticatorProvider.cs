using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Domain.ViewModels.Authenticate;
using _468_.Net_Fundamentals.Service.TokenGenerators;
using System;
using System.Collections.Generic;
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
            string accessToken = _accessTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDTO = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id

            };

            await _unitOfWork.Repository<RefreshToken>().InsertAsync(refreshTokenDTO);
            await _unitOfWork.SaveChangesAsync();


            return new AuthenticatedRespone
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
