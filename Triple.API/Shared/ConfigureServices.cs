using Triple.Application.Services;
using Triple.Application.Services.MailService;
using Triple.Infrastructure;
using Triple.Shared.Password;

namespace Triple.API.Shared
{
    public static class ConfigureSharedServices
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            //services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IRandomPasswordGenerator, RandomPasswordGenerator>();
            //services.AddScoped<IntegrationEventDispatcher>();

            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IImageService, ImageService>();

            services.AddTransient<IMailService, MailService>();
        }
    }
}
