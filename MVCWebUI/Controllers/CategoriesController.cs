#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVCWebUI.Controllers
{
	public class CategoriesController : Controller
	{
		// Add service injections here
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		// GET: Categories
		public IActionResult Index()
		{
			List<CategoryModel> categoryList = _categoryService.Query().ToList(); // TODO: Add get list service logic here
			return View(categoryList);
		}

		// GET: Categories/Details/5
		public IActionResult Details(int id)
		{
			CategoryModel category = _categoryService.Query().SingleOrDefault(c => c.Id == id); // TODO: Add get item service logic here
			if (category == null)
			{
				return View("_Error", "Category Not Found!");
			}
			return View(category);
		}

		// GET: Categories/Create
		public IActionResult Create()
		{
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			CategoryModel model = new CategoryModel()
			{
				Id = 0
			};
			return View(model);
		}

		// POST: Categories/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CategoryModel category)
		{
			if (ModelState.IsValid)
			{
				var result = _categoryService.Add(category);
				if (result.IsSuccessful)
				{
					TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Index));
				}
				ModelState.AddModelError("", result.Message);
			}
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			return View(category);
		}

		// GET: Categories/Edit/5
		public IActionResult Edit(int id)
		{
			CategoryModel category = _categoryService.Query().SingleOrDefault(c => c.Id == id); // TODO: Add get item service logic here
			if (category == null)
			{
				return View("_Error", "Category Not Found!");
			}
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			return View(category);
		}

		// POST: Categories/Edit
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(CategoryModel category)
		{
			if (ModelState.IsValid)
			{
				var result = _categoryService.Update(category);
				if (result.IsSuccessful)
				{
					TempData["Message"] = result.Message;
					//return RedirectToAction(nameof(Index));
					return RedirectToAction(nameof(Details), new { id = category.Id });
				}
				ModelState.AddModelError("", result.Message);
			}
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			return View(category);
		}

		// GET: Categories/Delete/5
		public IActionResult Delete(int id)
		{
			CategoryModel category = _categoryService.Query().SingleOrDefault(c => c.Id == id); // TODO: Add get item service logic here
			if (category == null)
			{
				return View("_Error", "Category Not Found!");
			}
			return View(category);
		}

		// POST: Categories/Delete
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var result = _categoryService.Delete(id);
			TempData["Message"] = result.Message;
			return RedirectToAction(nameof(Index));
		}
	}
}
