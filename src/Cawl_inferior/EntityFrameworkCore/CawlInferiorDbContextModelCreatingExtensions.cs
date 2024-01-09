using CawlInferior.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace CawlInferior.EntityFrameworkCore
{
    public static class CawlInferiorDbContextModelCreatingExtensions
    {
        public static void ConfigureCawlInferior(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<SpaceMarineDataSheet>(b =>
            {
                b.ToTable("SpaceMarineDataSheets");
                b.ConfigureByConvention();

                // Additional configurations specific to SpaceMarineDataSheet
                b.Property(sheet => sheet.Name).IsRequired().HasMaxLength(255);
                // Add more configurations for other properties...

                // Reference to SpaceMarineWeapon
                b.HasOne(sheet => sheet.MeleeWeapon)
                    .WithMany()
                    .HasForeignKey(sheet => sheet.MeleeWeaponId)
                    .IsRequired();

                b.HasOne(sheet => sheet.RangedWeapon)
                    .WithMany()
                    .HasForeignKey(sheet => sheet.RangedWeaponId)
                    .IsRequired();
            });

            builder.Entity<SpaceMarineWeapon>(b =>
            {
                b.ToTable("SpaceMarineWeapons");
                b.ConfigureByConvention();

                // Additional configurations specific to SpaceMarineWeapon
                b.Property(weapon => weapon.Name).IsRequired().HasMaxLength(255);
                // Add more configurations for other properties...
            });
        }
    }
}
