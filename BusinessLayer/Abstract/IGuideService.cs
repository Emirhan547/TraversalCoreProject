using DTOLayer.DTOs.GuideDtos;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGuideService
        : IGenericService<ResultGuideDto, CreateGuideDto, UpdateGuideDto>
    {
        Task ChangeGuideStatusToTrueAsync(int id);
        Task ChangeGuideStatusToFalseAsync(int id);
    }
}
