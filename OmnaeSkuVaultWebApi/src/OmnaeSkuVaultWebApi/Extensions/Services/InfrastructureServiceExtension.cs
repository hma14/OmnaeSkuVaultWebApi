namespace OmnaeSkuVaultWebApi.Extensions.Services;

using OmnaeSkuVaultWebApi.Databases;
using OmnaeSkuVaultWebApi.Resources;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IWebHostEnvironment env)
    {
        // DbContext -- Do Not Delete
        if (env.IsEnvironment(Consts.Testing.FunctionalTestingEnvName))
        {
            services.AddDbContext<FinanceServiceDbContext>(options =>
                options.UseInMemoryDatabase($"OmnaeFinanceServiceDb"));
        }
        else
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if(string.IsNullOrEmpty(connectionString))
            {
                // this makes local migrations easier to manage. feel free to refactor if desired.
                connectionString = env.IsDevelopment() 
                    ? "Data Source=localhost,50542;Integrated Security=False;Database=dev_omnaeskuvaultwebapi;User ID=SA;Password=#localDockerPassword#"
                    : throw new Exception("DB_CONNECTION_STRING environment variable is not set.");
            }

            services.AddDbContext<FinanceServiceDbContext>(options =>
                options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(FinanceServiceDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());
        }

        // Auth -- Do Not Delete
    }
}
