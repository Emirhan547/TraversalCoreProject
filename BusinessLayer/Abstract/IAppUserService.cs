using DTOLayer.DTOs.AppRoleDtos;
using DTOLayer.DTOs.AppUserDtos;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAppUserService:IGenericService<ResultAppRoleDto,CreateAppUserDto,UpdateAppUserDto>    
    {
    }
}
