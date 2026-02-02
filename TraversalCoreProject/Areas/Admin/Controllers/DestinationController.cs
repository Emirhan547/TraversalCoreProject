using BusinessLayer.Abstract;
using DTOLayer.DTOs.DestinatonDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Threading.Tasks;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public async Task<IActionResult> Index()
        {
            var values =await _destinationService.GetListAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddDestination()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDestination(DestinationInputDto destination)
        {
           await _destinationService.AddAsync(destination);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteDestination(int id)
        {
           await _destinationService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateDestination(int id)
        {
            var values = await _destinationService.GetByIdAsync(id);
            if (values is null)
            {
                return NotFound();
            }

            var model = new DestinationInputDto
            {
                Id = values.Id,
                City = values.City,
                DayNight = values.DayNight,
                Price = values.Price,
                Image = values.Image,
                Description = values.Description,
                Capacity = values.Capacity,
              
                CoverImage = values.CoverImage,
                Details1 = values.Details1,
                Details2 = values.Details2,
                Image2 = values.Image2,
  
                GuideId = values.GuideId
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateDestination(DestinationInputDto destination)
        {

          await _destinationService.UpdateAsync(destination);
            return RedirectToAction("Index");

        }
    }
}