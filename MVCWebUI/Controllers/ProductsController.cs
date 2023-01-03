using Microsoft.AspNetCore.Mvc;

namespace MVCWebUI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
