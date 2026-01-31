using AutoMapper;
using DTOLayer.DTOs.AnnouncementDTOs;
using DTOLayer.DTOs.AppUserDTOs;
using DTOLayer.DTOs.ContactDTOs;
using EntityLayer.Concrete;

namespace TraversalCoreProject.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreateAnnouncementDto, Announcement>();
            CreateMap<Announcement, CreateAnnouncementDto>();

 
            CreateMap<AppUserRegisterDTOs, AppUser>();
            CreateMap<AppUser, AppUserRegisterDTOs>();


            CreateMap<AppUserLoginDTOs, AppUser>();
            CreateMap<AppUser, AppUserLoginDTOs>();

            CreateMap<ResultAnnouncementDto, Announcement>();
            CreateMap<Announcement, ResultAnnouncementDto>();

            CreateMap<UpdateAnnouncementDto, Announcement>();
            CreateMap<Announcement, UpdateAnnouncementDto>();

            CreateMap<SendMessageDto, ContactUs>().ReverseMap();
            
        }

    }
}
