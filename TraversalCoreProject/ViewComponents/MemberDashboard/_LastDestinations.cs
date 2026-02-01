using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TraversalCoreProject.ViewComponents.MemberDashboard
{
    public class _LastDestinations:ViewComponent
    {
        private readonly IDestinationService _destinationService;

        public _LastDestinations(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values=await _destinationService.GetLast4DestinationsAsync();
            return View(values);
        }

    }
}
