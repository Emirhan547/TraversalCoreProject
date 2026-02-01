using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProject.Areas.Member.Controllers
{
    [Area("Member")]
    public class DashboardController : Controller
    {
        private readonly IAuthService _authService;

        public DashboardController(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task <IActionResult> Index()
        {
            if (User.Identity?.Name is not null)
            {
                var values = await _authService.GetByUserNameAsync(User.Identity.Name);
                if (values != null)
                {
                    ViewBag.userName = $"{values.Name} {values.Surname}";
                    ViewBag.userImage = values.ImageUrl;
                }
            }
            return View();
        }
        public async Task<IActionResult>MemberDashboard()
        {
            return View();
        }
    }
}
