using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.ContactDTOs;
using DTOLayer.DTOs.ContactUsDtos;
using EntityLayer.Concrete;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ContactUsManager : IContactUsService
    {
        private readonly IContactUsDal _contactUsDal;
        private readonly IUowDal _uowDal;

        public ContactUsManager(IUowDal uowDal, IContactUsDal contactUsDal)
        {
            _uowDal = uowDal;
            _contactUsDal = contactUsDal;
        }



        public async Task AddAsync(ContactMessageInputDto dto)
        {
            var entity = dto.Adapt<ContactUs>();
            entity.MessageStatus = true;
            entity.MessageDate = DateTime.Now;
            await _contactUsDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }



        public async Task UpdateAsync(ContactMessageInputDto dto)
        {
            var entity = await _contactUsDal.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("ContactUs not found");

            dto.Adapt(entity);
             _contactUsDal.UpdateAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

      

        public async Task DeleteAsync(int id)
        {
            var entity = await _contactUsDal.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("ContactUs not found");

            _contactUsDal.DeleteAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

       

        public async Task<ResultContactUsDto?> GetByIdAsync(int id)
        {
            var entity = await _contactUsDal.GetByIdAsync(id);
            return entity?.Adapt<ResultContactUsDto>();
        }

        public async Task<IReadOnlyList<ResultContactUsDto>> GetListAsync()
        {
            var entities = await _contactUsDal.GetListAsync();
            return entities.Adapt<IReadOnlyList<ResultContactUsDto>>();
        }

        public async Task<IReadOnlyList<ResultContactUsDto>> GetListByStatusAsync(bool status)
        {
            var entities = await _contactUsDal.GetListByStatusAsync(status);
            return entities.Adapt<IReadOnlyList<ResultContactUsDto>>();
        }


        public async Task ContactUsStatusChangeToFalseAsync(int id)
        {
            var entity = await _contactUsDal.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("ContactUs not found");

            entity.MessageStatus = false;
             _contactUsDal.UpdateAsync(entity);
            await _uowDal.SaveChangesAsync();
        }
    }
}
