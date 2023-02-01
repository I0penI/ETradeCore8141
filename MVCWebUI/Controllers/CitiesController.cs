using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVCWebUI.Controllers
{
    [Route("[controller]")] // ~/Cities
    public class CitiesController : Controller
    {
       
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [Route("[action]/{countryId?}")] // ~/Cities/Index/1
        public IActionResult Index(int? countryId) // ~/Cities/Index/1
        {
            var cities = _cityService.GetCities(countryId ?? 0);
            return Json(cities);
        }
    }
}
