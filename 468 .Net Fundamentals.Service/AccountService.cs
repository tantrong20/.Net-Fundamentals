using _468_.Net_Fundamentals.Domain.Base;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class AccountService : RepositoryBase<AppUser>, IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(ApplicationDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration) : base(context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Login(UserLoginVM userLoginVM)
        {
            var user = await _userManager.FindByEmailAsync(userLoginVM.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginVM.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSiginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(5),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256));

                return new OkObjectResult(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });     
            }
            return new UnauthorizedResult();
        }   

        public async Task<IActionResult> Register(UserRegistrationVM userRegistration)
        {
            var userExist = await _userManager.FindByEmailAsync(userRegistration.Email);

            if (userExist != null)
            {
                var resultStatus = new ObjectResult(new Response
                {
                    Status = "Error",
                    Message = "User Already Exist"
                });
                resultStatus.StatusCode = StatusCodes.Status500InternalServerError;
                return resultStatus;
            }

            var user = new AppUser
            {
                Email = userRegistration.Email,
                UserName = userRegistration.UserName,
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

            return new OkObjectResult(new Response
            {
                Status = "Success",
                Message = "User Created Success"
            });
        }

        public async Task<IActionResult> RegisterAdmin(UserRegistrationVM userRegistration)
        {
            var userExist = await _userManager.FindByEmailAsync(userRegistration.Email);

            if (userExist != null)
            {
                var resultStatus = new ObjectResult(new Response
                {
                    Status = "Error",
                    Message = "User Already Exist"
                });
                resultStatus.StatusCode = StatusCodes.Status500InternalServerError;
                return resultStatus;
            }

            var user = new AppUser
            {
                Email = userRegistration.Email,
                UserName = userRegistration.UserName,
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
            }

            if (!await _roleManager.RoleExistsAsync(AppUserRole.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(AppUserRole.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(AppUserRole.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(AppUserRole.User));
            }

            if(await _roleManager.RoleExistsAsync(AppUserRole.Admin))
            {
                await _userManager.AddToRoleAsync(user, AppUserRole.Admin);
            }

            return new OkObjectResult(new Response
            {
                Status = "Success",
                Message = "User Created Success"
            });
           
        }
    }
}
