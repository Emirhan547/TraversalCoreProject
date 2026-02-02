using BusinessLayer.Abstract;
using DTOLayer.DTOs.AppUserDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUserRegisterDtos model)
        {
            var appUser = new CreateAppUserDto
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Mail,
                UserName = model.UserName
            };

            if (model.Password == model.ConfirmPassword)
            {
                var result = await _authService.CreateUserAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Auth", new {area="Admin"});
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserSignInDto model, string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.PasswordSignInAsync(
                model.UserName,
                model.Password,
                false,
                true);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && returnUrl.StartsWith("/Admin"))
                    return LocalRedirect(returnUrl);

                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
            return View(model);
        }


    }
}