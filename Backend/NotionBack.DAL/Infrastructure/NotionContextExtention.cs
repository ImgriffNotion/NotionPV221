using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NotionBack.DAL.Infrastructure
{
    public static class NotionContextExtention
    {
        public static void AddNotionContext(
            this IServiceCollection services,
            string connectionString
        ) =>
            services.AddDbContext<NotionDbContext>(opt =>
                opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("NotionBack"))
            );
    }
}
