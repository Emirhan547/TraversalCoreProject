using DTOLayer.DTOs.AppUserDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        Task<ResultAppUserDto?> GetByUserNameAsync(string userName);
        Task<ResultAppUserDto?> GetByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(CreateAppUserDto dto, string password);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task<string> GeneratePasswordResetTokenAsync(int userId);
        Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword);
        Task<IdentityResult> UpdateProfileAsync(UpdateAppUserProfileDto dto);
    }
}
