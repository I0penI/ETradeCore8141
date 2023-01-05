﻿using System.Text;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MVCWebUI.Areas.Database.Controllers
{
	[Area("Db")]
	public class HomeController : Controller
	{
		private readonly ETradeContext _db;
		public HomeController(ETradeContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			try
			{
				var products = _db.Products.ToList();
				_db.Products.RemoveRange(products);

				var categories = _db.Categories.ToList();
				_db.Categories.RemoveRange(categories);

				_db.Categories.Add(new Category()
				{
					Name = "Computer",
					Description = "Laptops, desktops and computer peripherals",
					Products = new List<Product>()
				{
					new Product()
					{
						Name = "Laptop",
						UnitPrice = 3000,
						ExpirationDate = new DateTime(2032, 1, 27),
						StockAmount = 10
					},
					new Product()
					{
						Name = "Mouse",
						UnitPrice = 20.5,
						StockAmount = 50,
						Description = "Computer peripheral"
					},
					new Product()
					{
						Name = "Keyboard",
						UnitPrice = 40,
						StockAmount = 45,
						Description = "Computer peripheral"
					},
					new Product()
					{
						Name = "Monitor",
						UnitPrice = 2500,
						ExpirationDate = DateTime.Parse("19.05.2023"),
						StockAmount = 20,
						Description = "Computer peripheral"
					}
				}
				});

				_db.Categories.Add(new Category()
				{
					Name = "Home Theater System",
					Products = new List<Product>()
				{
					new Product()
					{
						Name = "Speaker",
						UnitPrice = 2500,
						StockAmount = 70
					},
					new Product()
					{
						Name = "Receiver",
						UnitPrice = 5000,
						StockAmount = 30,
						Description = "Home theater system component"

					},
					new Product()
					{
						Name = "Equalizer",
						UnitPrice = 1000,
						StockAmount = 40
					}
				}
				});
				_db.SaveChanges();
				return Content("<label style=\"color:darkgreen;\"><b>Database seed successful.<b></label>", "text/html", Encoding.UTF8);

			}
			catch (Exception exc)
			{
				string message = exc.Message;
				throw exc;
			}
		}
	}
}
