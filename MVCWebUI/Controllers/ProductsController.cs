using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVCWebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.Query().ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
