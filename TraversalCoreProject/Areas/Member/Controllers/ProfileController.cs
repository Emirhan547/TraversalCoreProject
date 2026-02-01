using BusinessLayer.Abstract;
using DTOLayer.DTOs.AppUserDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TraversalCoreProject.Areas.Member.Models;

namespace TraversalCoreProject.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly IAuthService _authService;

        public ProfileController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity?.Name is null)
            {
                return View(new UserEditViewModel());
            }

            var values = await _authService.GetByUserNameAsync(User.Identity.Name);
            if (values == null)
            {
                return View(new UserEditViewModel());
            }
            UserEditViewModel userEditViewModel = new UserEditViewModel();


            userEditViewModel.name = values.Name;
            userEditViewModel.surname = values.Surname;
            userEditViewModel.phonenumber = values.PhoneNumber;
            userEditViewModel.mail = values.Email;
            
            return View(userEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            if (User.Identity?.Name is null)
            {
                return View();
            }

            var user = await _authService.GetByUserNameAsync(User.Identity.Name);
            if (user == null)
            {
                return View();
            }

            var profileDto = new UpdateAppUserProfileDto
            {
                Id = user.Id,
                Name = p.name,
                Surname = p.surname,
                Email = p.mail,
                PhoneNumber = p.phonenumber,
                Password = p.password
            };
            if (p.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.Image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/userimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await p.Image.CopyToAsync(stream);
                profileDto.ImageUrl = imagename;
            }
            var result = await _authService.UpdateProfileAsync(profileDto);
            
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login");
            }
            return View();

        }
        }
    }



