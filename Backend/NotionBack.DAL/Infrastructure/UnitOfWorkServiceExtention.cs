using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace NotionBack.DAL.Infrastructure
{
    public static class UnitOfWorkServiceExtention
    {
        public static void AddUnitOfWorkService(this IServiceCollection services) =>
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
    }
}
