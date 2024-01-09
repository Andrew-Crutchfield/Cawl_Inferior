using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CawlInferior.EntityFrameworkCore
{
    [DependsOn(
        typeof(CawlInferiorDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class CawlInferiorEntityFrameworkModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CawlInferiorDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        }
    }
}