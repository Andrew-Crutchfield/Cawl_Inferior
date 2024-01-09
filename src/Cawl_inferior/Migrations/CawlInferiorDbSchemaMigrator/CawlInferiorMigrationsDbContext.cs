// File: CawlInferior.EntityFrameworkCore/CawlInferiorMigrationsDbContext.cs
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CawlInferior.EntityFrameworkCore
{
    [ConnectionStringName(CawlInferiorDbProperties.ConnectionStringName)]
    public class CawlInferiorMigrationsDbContext : AbpDbContext<CawlInferiorMigrationsDbContext>
    {
        public CawlInferiorMigrationsDbContext(DbContextOptions<CawlInferiorMigrationsDbContext> options)
            : base(options)
        {
        }
    }
}
