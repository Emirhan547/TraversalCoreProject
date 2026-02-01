using BusinessLayer.Abstract;
using DTOLayer.DTOs.AppUserDtos;
using EntityLayer.Concrete;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ResultAppUserDto?> GetByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user?.Adapt<ResultAppUserDto?>();
        }

        public async Task<ResultAppUserDto?> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user?.Adapt<ResultAppUserDto?>();
        }

        public async Task<IdentityResult> CreateUserAsync(CreateAppUserDto dto, string password)
        {
            var user = dto.Adapt<AppUser>();
            return await _userManager.CreateAsync(user, password);
        }

        public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return string.Empty;
            }

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return IdentityResult.Failed();
            }

            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<IdentityResult> UpdateProfileAsync(UpdateAppUserProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
            {
                return IdentityResult.Failed();
            }

            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            if (!string.IsNullOrWhiteSpace(dto.ImageUrl))
            {
                user.ImageUrl = dto.ImageUrl;
            }

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);
            }

            return await _userManager.UpdateAsync(user);
        }
    }
}