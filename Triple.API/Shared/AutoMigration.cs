using Triple.Infrastructure.Identity;
using Triple.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Triple.Infrastructure.Persistence;

namespace Triple.API.Shared
{
    public static class AutoMigration
    {
        public static void Initialize(IApplicationBuilder app, IConfiguration configuration)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceProvider>().CreateScope())
            {
                using (var db = scope.ServiceProvider.GetService<TripleDbContext>())
                {
                    db.Database.Migrate();
                    Seed.Initialize(db, configuration, scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(), scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>());
                }
            }
        }
    }
}
