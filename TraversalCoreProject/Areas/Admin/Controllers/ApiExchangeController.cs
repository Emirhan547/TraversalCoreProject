using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DTOLayer.DTOs.ExternalApiDtos;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    public class ApiExchangeController : Controller
    {
        [Area("Admin")]
        [AllowAnonymous]
        public async Task <IActionResult> Index()
        {
           
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/attractions/calendar?attraction_id=PRFZkGSVnM5d&currency=AED&locale=en-gb"),
                Headers =
    {
        { "x-rapidapi-key", "047a17087bmsh5d1af7361331700p13e5fdjsne8c480915cd5" },
        { "x-rapidapi-host", "booking-com.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<BookingExchangeDto>(body);
                return View(values.exchange_rates);
            }
        }
    }
}

