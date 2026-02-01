using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.AnnouncementDtos;
using EntityLayer.Concrete;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AnnouncementManager : IAnnouncementService
    {
        private readonly IAnnouncementDal _announcementDal;
        private readonly IUowDal _uowDal;
        public AnnouncementManager(IAnnouncementDal announcementDal, IUowDal uowDal)
        {
            _announcementDal = announcementDal;
            _uowDal = uowDal;
        }

        public async Task AddAsync(CreateAnnouncementDto dto)
        {
            var values = dto.Adapt<Announcement>();
           await _announcementDal.AddAsync(values);
           await _uowDal.SaveChangesAsync();

        }
        public async Task<UpdateAnnouncementDto?> GetUpdateDtoAsync(int id)
        {
            var values = await _announcementDal.GetByIdAsync(id);
            return values?.Adapt<UpdateAnnouncementDto?>();
        }
        public async Task DeleteAsync(int id)
        {
            var values = await _announcementDal.GetByIdAsync(id);

            _announcementDal.DeleteAsync(values);
            await _uowDal.SaveChangesAsync();
        }

        public async Task<ResultAnnouncementDto?> GetByIdAsync(int id)
        {
            var values = await _announcementDal.GetByIdAsync(id);
            
            return values?.Adapt<ResultAnnouncementDto?>();
        }

        public async Task<IReadOnlyList<ResultAnnouncementDto>> GetListAsync()
        {
            var values=await _announcementDal.GetListAsync();
            return values.Adapt<IReadOnlyList<ResultAnnouncementDto>>();


        }

        public async Task UpdateAsync(UpdateAnnouncementDto dto)
        {
            var values=await _announcementDal.GetByIdAsync(dto.Id);
            
            dto.Adapt(values);
            _announcementDal.Adapt(values);
            await _uowDal.SaveChangesAsync();
            
            
        }
    }
}
