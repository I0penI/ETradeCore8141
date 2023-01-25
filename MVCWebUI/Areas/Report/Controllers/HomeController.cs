using Business.Services;
using Business.Services.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWebUI.Areas.Report.Models;

namespace MVCWebUI.Areas.Report.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Report")]
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ICategoryService _categoryService;

		public HomeController(IReportService reportService, ICategoryService categoryService)
		{
			_reportService = reportService;
			_categoryService = categoryService;
		}

		public IActionResult Index(HomeIndexViewModel viewModel)
        {
            viewModel.Report = _reportService.GetReport(viewModel.Filter,true);
            viewModel.Categories = new SelectList(_categoryService.Query().ToList(), "Id", "Name");
			return View(viewModel);
        }
    }
}
