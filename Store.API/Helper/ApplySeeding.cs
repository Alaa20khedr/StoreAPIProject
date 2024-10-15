using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities.IdentityEntity;
using Store.Reposatory;

namespace Store.API.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using(var scope=app.Services.CreateScope())
            {
                var services=scope.ServiceProvider;
                var loggerfactory=services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context=services.GetRequiredService<StoreContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerfactory);
                    await AppIdentityContextSeed.SeedUserAsync(userManager);

                }
                catch (Exception ex)
                {
                    var logger = loggerfactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);

                }
            }

        }
    }
}
