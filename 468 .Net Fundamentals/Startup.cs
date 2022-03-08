using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using _468_.Net_Fundamentals.Extensions;
using _468_.Net_Fundamentals.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using _468_.Net_Fundamentals.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using _468_.Net_Fundamentals.Service.TokenGenerators;
using _468_.Net_Fundamentals.Service.TokenValidators;
using _468_.Net_Fundamentals.Service.Authenticator;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using _468_.Net_Fundamentals.Controllers.Authorization;
using _468_.Net_Fundamentals.Service.LogActivity;

namespace _468_.Net_Fundamentals
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
   
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                      options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();

            // Extensions
            services
                .AddDatabase(Configuration)
                .AddRepositories()
                .AddServices()
                .AddCurrentUser();

            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddSingleton<GetPrincipal>();
            services.AddScoped<AuthenticatorProvider>();
            services.AddScoped<UserActivityLoger>();

            // For Identity
            services.AddIdentity<AppUser, IdentityRole>(config =>
                {
                    config.Password.RequireNonAlphanumeric = false; //optional
                    config.SignIn.RequireConfirmedEmail = false; //optional
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero,
                     ValidAudience = Configuration["JWT:ValidAudience"],
                     ValidIssuer = Configuration["JWT:ValidIssuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                 };
                 options.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         context.NoResult();
                         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                         context.Response.ContentType = "application/json";

                         string response =
                             JsonConvert.SerializeObject("The access token provided is not valid.");
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                             response =
                                 JsonConvert.SerializeObject("The access token provided has expired.");
                         }

                         context.Response.WriteAsync(response);
                         return Task.CompletedTask;
                     }
                 };
             });


            // Register it as scope, because it uses Repository that probably uses dbcontext
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "468 .Net Fundamentals", Version = "1.0" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable the Cross-Origin Requests
            app.UseCors(option => option
               .AllowAnyOrigin() // Any Origin
               .AllowAnyMethod() // Any Method GET, PUT, POST, DELETE
               .AllowAnyHeader() // Any Header
               .WithExposedHeaders("*")
             );

            app.UseExceptionMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "468 .Net Fundamentals (V 1.0)");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();   
            app.UseAuthorization();   

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
