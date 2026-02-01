using DTOLayer.DTOs.AppRoleDtos;
using DTOLayer.DTOs.AppUserDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAppRoleService
    {
        Task<IReadOnlyList<ResultAppRoleDto>> GetListAsync();
        Task<ResultAppRoleDto?> GetByIdAsync(int id);
        Task<IdentityResult> CreateAsync(CreateAppRoleDto dto);
        Task<IdentityResult> UpdateAsync(UpdateAppRoleDto dto);
        Task<IdentityResult> DeleteAsync(int id);
        Task<IReadOnlyList<ResultAppUserDto>> GetUsersAsync();
        Task<IReadOnlyList<RoleAssignDto>> GetRoleAssignmentsAsync(int userId);
        Task UpdateRoleAssignmentsAsync(int userId, IReadOnlyList<RoleAssignDto> assignments);
    }
}
