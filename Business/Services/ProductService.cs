using System.Globalization;
using AppCore.Business.Services.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Repostitories;

namespace Business.Services
{

    public interface IProductService : IService<ProductModel>
    {

    }
    public class ProductService : IProductService
    {

        private readonly ProductRepoBase _repo;

        public ProductService(ProductRepoBase repo)
        {
            _repo = repo;
        }

        public Result Add(ProductModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _repo.Dispose();
        }

        public IQueryable<ProductModel> Query()
        {
            return _repo.Query(p => p.Category).Select(p => new ProductModel()
            { // AutoMapper
                CategoryId = p.CategoryId,
                Description = p.Description,
                ExpirationDate = p.ExpirationDate,
                Guid = p.Guid,
                Id = p.Id,
                Name = p.Name,
                StockAmount = p.StockAmount,
                UnitPrice = p.UnitPrice,
                UnitPriceDisplay = p.UnitPrice.ToString("C2", new CultureInfo("en-US")), //tr-TR
                ExpirationDateDisplay = p.ExpirationDate != null ? p.ExpirationDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : "",
				//2.yol
                //ExpirationDateDisplay = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : "",

                CategoryNameDisplay = p.Category.Name


			});

        }

        public Result Update(ProductModel model)
        {
            throw new NotImplementedException();
        }
    }
}
