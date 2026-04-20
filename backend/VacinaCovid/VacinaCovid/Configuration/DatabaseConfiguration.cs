using Microsoft.EntityFrameworkCore;
using VacinaCovid.Repositorio.Context;

namespace VacinaCovid.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<VacinaCovidContext>(options =>
                options.UseSqlServer(connectionString)
            );
        }
    }
}