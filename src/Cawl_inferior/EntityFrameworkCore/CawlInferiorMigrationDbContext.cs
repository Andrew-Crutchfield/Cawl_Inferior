using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CawlInferior.EntityFrameworkCore
{
    [ConnectionStringName("CawlInferiorMigration")]
    public class CawlInferiorMigrationDbContext : AbpDbContext<CawlInferiorMigrationDbContext>
    {
        public CawlInferiorMigrationDbContext(DbContextOptions<CawlInferiorMigrationDbContext> options)
            : base(options)
        {
        }
    }
}