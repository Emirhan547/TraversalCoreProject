using DTOLayer.DTOs.ContactDTOs;
using DTOLayer.DTOs.ContactUsDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IContactUsService : IGenericService<ResultContactUsDto, ContactMessageInputDto, ContactMessageInputDto>

    {
        Task<IReadOnlyList<ResultContactUsDto>> GetListByStatusAsync(bool status);
        Task ContactUsStatusChangeToFalseAsync(int id);
      
    }
}
