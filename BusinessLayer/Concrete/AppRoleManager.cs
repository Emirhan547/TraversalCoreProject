using BusinessLayer.Abstract;
using DTOLayer.DTOs.AppRoleDtos;
using DTOLayer.DTOs.AppUserDtos;
using EntityLayer.Concrete;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AppRoleManager : IAppRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AppRoleManager(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IReadOnlyList<ResultAppRoleDto>> GetListAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Adapt<IReadOnlyList<ResultAppRoleDto>>();
        }

        public async Task<ResultAppRoleDto?> GetByIdAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return role?.Adapt<ResultAppRoleDto?>();
        }

        public async Task<IdentityResult> CreateAsync(CreateAppRoleDto dto)
        {
            var role = dto.Adapt<AppRole>();
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> UpdateAsync(UpdateAppRoleDto dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.Id.ToString());
            if (role == null)
            {
                return IdentityResult.Failed();
            }

            role.Name = dto.Name;
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return IdentityResult.Failed();
            }

            return await _roleManager.DeleteAsync(role);
        }

        public async Task<IReadOnlyList<ResultAppUserDto>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Adapt<IReadOnlyList<ResultAppUserDto>>();
        }

        public async Task<IReadOnlyList<RoleAssignDto>> GetRoleAssignmentsAsync(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return new List<RoleAssignDto>();
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            var assignments = new List<RoleAssignDto>();

            foreach (var role in roles)
            {
                assignments.Add(new RoleAssignDto
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    RoleExist = userRoles.Contains(role.Name)
                });
            }

            return assignments;
        }

        public async Task UpdateRoleAssignmentsAsync(int userId, IReadOnlyList<RoleAssignDto> assignments)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return;
            }

            foreach (var item in assignments)
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
        }
    }
}