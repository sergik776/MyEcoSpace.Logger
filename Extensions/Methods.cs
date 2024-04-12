using Microsoft.Extensions.DependencyInjection;
using MyEcoSpace.Logger.Configurations;
using MyEcoSpace.Logger.Interfaces;
using MyEcoSpace.Logger.Models.Config;
using MyEcoSpace.Logger.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Extensions
{
    public static class Methods
    {
        public static IServiceCollection AddILogger<T>(this IServiceCollection services)
        {
            var c = ConfigParser.GetGonfig();
            services.AddSingleton<ILogger<T>>(new MainLogger<T>(c));
            return services;
        }
    }
}
