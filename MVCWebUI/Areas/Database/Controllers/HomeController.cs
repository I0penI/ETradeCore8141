using Microsoft.AspNetCore.Mvc;

namespace MVCWebUI.Areas.Database.Controllers
{
	[Area("Db")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return Content("Yeter bitsin artık!");
		}
	}
}
