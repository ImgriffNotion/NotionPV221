using NotionBack.Services.EmailAuthorizationService;
using NotionBack.Services.EmailService;
using NotionBack.Services.RandomService;

namespace NotionBack.Services
{
    public static class NotionBackServiceExtentions
    {
        public static IServiceCollection RegistatorAllServices(this IServiceCollection services)
        {
            services.AddSingleton<IRandomService, RandomCreatorService>();
            services.AddSingleton<IEmailService, EmailSenderService>();
            services.AddScoped<IEmailAuthService, EmailAuthService>();
            services.AddHttpClient();
            return services;
        }
    }
}
