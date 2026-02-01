using DTOLayer.DTOs.ContactDTOs;
using DTOLayer.DTOs.ContactUsDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IContactUsService
        : IGenericService<ResultContactUsDto, CreateContactUsDto, UpdateContactUsDto>
    {
        Task<IReadOnlyList<ResultContactUsDto>> GetListContactUsByTrueAsync();
        Task<IReadOnlyList<ResultContactUsDto>> GetListContactUsByFalseAsync();
        Task ContactUsStatusChangeToFalseAsync(int id);
        Task AddAsync(SendMessageDto dto);
    }
}
