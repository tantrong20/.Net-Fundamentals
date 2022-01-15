using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Infrastructure;
using _468_.Net_Fundamentals.Infrastructure.Repositories;
using _468_.Net_Fundamentals.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace _468_.Net_Fundamentals.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime   
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            }
            );

            services.AddScoped<Func<ApplicationDbContext>>((provider) => () => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork, UnitOfWorkBase>();


            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>)); 
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ICardService, CardService>()
                .AddScoped<IProjectService, ProjectService>()
                .AddScoped<ITodoService, TodoService>()
                .AddScoped<ITagService, TagService>()
                .AddScoped<IBusinessService, BusinessService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IActivityService, ActivityService>();



        }
    }
}
