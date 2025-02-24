using Microsoft.Extensions.DependencyInjection;
using NotionBack.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotionBack.DAL
{
    public static class NotionBackDALServiceExtentions
    {
        public static IServiceCollection TMP(this IServiceCollection services)
        {
            services.AddScoped<IExample, Example>();
            return services;
        }
    }
}
