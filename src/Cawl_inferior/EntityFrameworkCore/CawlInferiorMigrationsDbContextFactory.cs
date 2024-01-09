using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CawlInferior.EntityFrameworkCore
{
    public class CawlInferiorMigrationsDbContextFactory : IDesignTimeDbContextFactory<CawlInferiorMigrationsDbContext>
    {
        public CawlInferiorMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<CawlInferiorMigrationsDbContext>()
                .UseMySql(configuration.GetConnectionString("Default"));

            return new CawlInferiorMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
    }

    public class CawlInferiorMigrationsDbContext : CawlInferiorDbContext
    {
        public CawlInferiorMigrationsDbContext(DbContextOptions<CawlInferiorMigrationsDbContext> options)
            : base(options)
        {

        }
    }
}