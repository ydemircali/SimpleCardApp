using Microsoft.Extensions.DependencyInjection;
using SimpleCardApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCardApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<MongoDbService>();
            services.AddSingleton<RedisService>();

            return services;
        }

    }
}
