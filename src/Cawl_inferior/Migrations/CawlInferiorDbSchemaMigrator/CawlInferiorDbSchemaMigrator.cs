// File: CawlInferior.DbMigrator/CawlInferiorDbSchemaMigrator.cs
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace CawlInferior.DbMigrator
{
    public class CawlInferiorDbSchemaMigrator
    {
        private readonly IServiceProvider _serviceProvider;

        public CawlInferiorDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Migrate()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var databaseProvider = scope.ServiceProvider.GetRequiredService<IDatabaseProvider>();
                    var migrator = databaseProvider.GetDbMigrator();

                    migrator.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<CawlInferiorDbSchemaMigrator>>();
                    logger.LogError(ex, "An error occurred while migrating the database schema.");
                    throw new AbpException("Failed to migrate the database schema.", ex);
                }
            }
        }
    }
}
