using BusinessLayer.Abstract;
using DTOLayer.DTOs.AppUserDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


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
                return View(new UserEditDto());
            }

            var values = await _authService.GetByUserNameAsync(User.Identity.Name);
            if (values == null)
            {
                return View(new UserEditDto());
            }
            UserEditDto userEditViewModel = new UserEditDto();

            userEditViewModel.Name = values.Name;
            userEditViewModel.Surname = values.Surname;
            userEditViewModel.PhoneNumber = values.PhoneNumber;
            userEditViewModel.Mail = values.Email;

            return View(userEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditDto p)
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
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Mail,
                PhoneNumber = p.PhoneNumber,
                Password = p.Password
            };
            if (Request.Form.Files.Count > 0)
            {
                var imageFile = Request.Form.Files[0];
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(imageFile.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/userimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await imageFile.CopyToAsync(stream);
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



