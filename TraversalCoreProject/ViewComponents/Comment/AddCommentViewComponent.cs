using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TraversalCoreProject.Models;


namespace TraversalCoreProje.ViewComponents.Comment
{
    public class AddCommentViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public AddCommentViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int destinationId)
        {
            var model = new AddCommentViewModel
            {
                DestinationId = destinationId
            };

            if (User?.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    model.AppUserId = user.Id;
                }
            }

            return View(model);
        }
    }
}