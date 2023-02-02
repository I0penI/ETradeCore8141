    #nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
    using DataAccess.Contexts;
    using DataAccess.Entities;
using Business.Services;
using Business.Models;

namespace WepApi.Controllers
{
    [Route("api/[controller]")] // ~/api/conrtoller
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Add service injections here
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult Get()
        {
            List<ProductModel> productList = _productService.Query().ToList(); // TODO: Add get list service logic here
            return Ok(productList); // Ok = 200 durum kodu
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ProductModel product = _productService.Query().SingleOrDefault(p => p.Id == id); // TODO: Add get item service logic here
			if (product == null)
            {
                return NotFound(); // 404
            }
			return Ok(product); // 200
        }

		// POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var result = _productService.Add(product);
                if (result.IsSuccessful)
                {
					return CreatedAtAction("Get", new { id = product.Id }, product); // 201
				}                  
                ModelState.AddModelError("", result.Message); 
            }
            return BadRequest(ModelState); // 400
        }

        // PUT: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(ProductModel product)
        {
			if (ModelState.IsValid)
			{
				var result = _productService.Update(product);
				if (result.IsSuccessful)
				{
					return NoContent(); // 204
                    // return Ok(product); 
				}
				ModelState.AddModelError("", result.Message);
			}
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
			//return BadRequest(ModelState); // 400
			
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);

            return NoContent();
        }
	}
}
