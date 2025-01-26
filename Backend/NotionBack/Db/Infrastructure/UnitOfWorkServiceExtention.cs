using NotionBack.Db.Interfaces;
using NotionBack.Db.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace NotionBack.Db.Infrastructure
{
    public static class UnitOfWorkServiceExtention
    {
        public static void AddUnitOfWorkService(this IServiceCollection services) =>
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
    }
}
