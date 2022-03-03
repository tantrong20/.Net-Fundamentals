using _468_.Net_Fundamentals.Domain.Base;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Domain.ViewModels.Authenticate;
using _468_.Net_Fundamentals.Domain.ViewModels.Request;
using _468_.Net_Fundamentals.Infrastructure;
using _468_.Net_Fundamentals.Service.Authenticator;
using _468_.Net_Fundamentals.Service.TokenGenerators;
using _468_.Net_Fundamentals.Service.TokenValidators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly GetPrincipal _getPrincipal;
        private readonly AuthenticatorProvider _authenticator;


        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, RefreshTokenValidator refreshTokenValidator, AuthenticatorProvider authenticator,GetPrincipal getPrincipal)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _refreshTokenValidator = refreshTokenValidator;
            _authenticator = authenticator;
            _getPrincipal = getPrincipal;
        }


        public async Task<IActionResult> Login(UserLoginVM userLoginVM)
        {
            var user = await _userManager.FindByEmailAsync(userLoginVM.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginVM.Password))
            {

                AuthenticatedRespone respone = await _authenticator.Authenticate(user);

                return new OkObjectResult(respone);

            }
            return new UnauthorizedResult();
        }

        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            string accessToken = request.Token;
            string refreshToken = request.RefreshToken;

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshToken);

            if (!isValidRefreshToken)
            {
                return new BadRequestResult();
            }

            var principal = _getPrincipal.FromExpiredToken(accessToken);
            var user = await GetUserFromPrincipal(principal);

            if (user == null)
            {
                return new NotFoundResult();
            }

            AuthenticatedRespone respone = await _authenticator.Authenticate(user);

            return new OkObjectResult(respone);
        }



        public async Task<IActionResult> Register(UserRegistrationVM userRegistration)
        {
            // Validate user model
            var validated = this.ValidateUser(userRegistration);

            if (!await validated)
            {
                var resultStatus = new ObjectResult(new Response
                {
                    Status = "Error",
                    Message = "User Already Exist"
                });
                resultStatus.StatusCode = StatusCodes.Status500InternalServerError;
                return resultStatus;
            }

            // Register new user
            var user = new AppUser
            {
                UserName = userRegistration.UserName,
                Email = userRegistration.Email,
                ImagePath = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png",
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if (!result.Succeeded)
            {
                var resultStatus = new ObjectResult(new Response
                {
                    Status = "Error",
                    Message = "User Already Exist"
                });
                resultStatus.StatusCode = StatusCodes.Status500InternalServerError;
                return resultStatus;
            }


            if (await _roleManager.RoleExistsAsync(Roles.Basic.ToString()))
            {
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
            }

            return new OkObjectResult(new Response
            {
                Status = "Success",
                Message = "User Created Success"
            });
        }

  

        private async Task<bool> ValidateUser(UserRegistrationVM userRegistration)
        {
            var userExist = await _userManager.FindByEmailAsync(userRegistration.Email);

            if (userExist != null)
            {
                return false;
            }

            return true;
        }


        public async Task<AppUser> GetUserFromPrincipal(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var userNameClaim = claim
                .Where(x => x.Type == "Name")
                .FirstOrDefault().Value;

            AppUser user = await _userManager.FindByNameAsync(userNameClaim);

            return user;
        }

    }
}
