using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CawlInferior.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class CawlInferiorDbContext : AbpDbContext<CawlInferiorDbContext>
    {
        public DbSet<SpaceMarineDataSheet> SpaceMarineDataSheets { get; set; }
        public DbSet<SpaceMarineWeapon> SpaceMarineWeapons { get; set; }

        public CawlInferiorDbContext(DbContextOptions<CawlInferiorDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureCawlInferior();
        }
    }
}
