using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DTOLayer.DTOs.DestinatonDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Threading.Tasks;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<IActionResult> AddDestination(CreateDestinationDto destination)
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

            var model = new UpdateDestinationDto
            {
                Id = values.Id,
                City = values.City,
                DayNight = values.DayNight,
                Price = values.Price,
                Image = values.Image,
                Description = values.Description,
                Capacity = values.Capacity,
                Status = values.Status,
                CoverImage = values.CoverImage,
                Details1 = values.Details1,
                Details2 = values.Details2,
                Image2 = values.Image2,
                Date = values.Date,
                GuideId = values.GuideId
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateDestination(UpdateDestinationDto destination)
        {

          await _destinationService.UpdateAsync(destination);
            return RedirectToAction("Index");

        }
    }
}