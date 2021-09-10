using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartCity.CitizenAccount.Data.Access.DAL;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Security;
using SmartCity.CitizenAccount.Security.Auth;
using SmartCity.CitizenAccount.Services;
using SmartCity.CitizenAccount.Services.CitizenAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.IoC
{
    public static class ContainerSetup
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureAuth(services);
            AddRepositories(services, configuration);
            AddAppServices(services);
        }

        private static void ConfigureAuth(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<ISecurityContext, SecurityContext>();
        }

        private static void AddRepositories(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Data:main"];

            services.AddDbContext<MainDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IGenericRepository, GenericRepository>();
        }

        private static void AddAppServices(IServiceCollection services)
        {
            var exampleServiceType = typeof(ExampleAppService);
            var types = (from t in exampleServiceType.GetTypeInfo().Assembly.GetTypes()
                         where t.Namespace.Contains(exampleServiceType.Namespace) && t.Namespace != exampleServiceType.Namespace
                            && t.GetTypeInfo().IsClass
                            && t.GetTypeInfo().GetCustomAttribute<CompilerGeneratedAttribute>() == null
                         select t).ToArray();

            foreach (var type in types)
            {
                var interfaceQ = type.GetTypeInfo().GetInterfaces().First();
                services.AddScoped(interfaceQ, type);
            }
        }
    }
}
