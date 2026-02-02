using DTOLayer.DTOs.DestinatonDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IDestinationService
: IGenericService<ResultDestinationDto, DestinationInputDto, DestinationInputDto>
    {
        Task<ResultDestinationDto?> GetDestinationWithGuideAsync(int id);
        Task<IReadOnlyList<ResultDestinationDto>> GetLast4DestinationsAsync();
    }
}
