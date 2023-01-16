using AppCore.Results.Bases;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCWebUI.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;

		public ProductsController(IProductService productService, ICategoryService categoryService)
		{
			_productService = productService;
			_categoryService = categoryService;
		}

		public IActionResult Index()
		{
			var products = _productService.Query().ToList();
			return View(products);
		}

		[HttpGet] // yazmaya gerek yok default olarak get geliyor 
		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_categoryService.Query().ToList(), "Id", "Name");
			return View();
		}

		[HttpPost]
		// public IActionResult Create(string Name, string Description, double UnitPrice, int StockAmount, DateTime? ExpirationDate, int CategoryId ) 
		public IActionResult Create(ProductModel product)
		{
			if (ModelState.IsValid)
			{
				Result result = _productService.Add(product);
				if (result.IsSuccessful)
				{
					//return RedirectToAction("Index", "Products");
					//return RedirectToAction("Index");
					TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Index));
				}
				ViewData["Message"] = result.Message; // error

			}
			ViewBag.Categories = new SelectList(_categoryService.Query().ToList(), "Id", "Name", product.CategoryId);
			return View(product);
		}

		public IActionResult Edit(int id) // controller/action/id?
		{
			var product = _productService.Query().SingleOrDefault(p => p.Id == id);
			if (product is null)
				return NotFound();
			ViewBag.CategoryId = new SelectList(_categoryService.Query().ToList(), "Id", "Name", product.CategoryId);
			return View(product);
			
		}
	}
}
