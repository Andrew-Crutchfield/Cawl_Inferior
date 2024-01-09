using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace CawlInferior.SpaceMarines
{
    public interface ISpaceMarineAppService : IApplicationService
    {
        Task<List<SpaceMarineDto>> GetAllSpaceMarinesAsync();
        Task<SpaceMarineDto> GetSpaceMarineByIdAsync(int id);
    }
}
