﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services;
using Business.Models;
using Microsoft.AspNetCore.Authorization;

namespace MVCWebUI.Controllers
{
	[Authorize(Roles = "Admin")]
	public class StoresController : Controller
	{
		// Add service injections here
		private readonly IStoreService _storeService;

		public StoresController(IStoreService storeService)
		{
			_storeService = storeService;
		}

		// GET: Stores
		public IActionResult Index()
		{
			List<StoreModel> storeList = _storeService.Query().ToList();
			return View(storeList);
		}

		// GET: Stores/Details/5
		public IActionResult Details(int id)
		{
			StoreModel store = _storeService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
			if (store == null)
			{
				return View("_Error", "Store Not Found");
			}
			return View(store);
		}

		// GET: Stores/Create
		public IActionResult Create()
		{
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			return View();
		}

		// POST: Stores/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(StoreModel store)
		{
			if (ModelState.IsValid)
			{
				var result = _storeService.Add(store);
				if (result.IsSuccessful)
					return RedirectToAction(nameof(Index));
				ModelState.AddModelError("", result.Message);
			}
			return View(store);
		}

		// GET: Stores/Edit/5
		public IActionResult Edit(int id)
		{
			StoreModel store = _storeService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
			if (store == null)
			{
				return View("_Error" , "Store Not Found!");
			}
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			return View(store);
		}

		// POST: Stores/Edit
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(StoreModel store)
		{
			if (ModelState.IsValid)
			{
				var result = _storeService.Update(store);
				if (result.IsSuccessful)
					return RedirectToAction(nameof(Index));
				ModelState.AddModelError("", result.Message);
			}
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			return View(store);
		}

		// GET: Stores/Delete/5
		public IActionResult Delete(int id)
		{
			StoreModel store = _storeService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
			if (store == null)
			{
				return View("_Error", "Store Not Found!");
			}
			return View(store);
		}

		// POST: Stores/Delete
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			_storeService.Delete(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
