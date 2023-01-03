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
            return _repo.Query().Select(p => new ProductModel()
            { // AutoMapper
                CategoryId = p.CategoryId,
                Description = p.Description,
                ExpirationDate = p.ExpirationDate,
                Guid = p.Guid,
                Id = p.Id,
                Name = p.Name,
                StockAmount = p.StockAmount,
                UnirPrice = p.UnirPrice
            });
        }

        public Result Update(ProductModel model)
        {
            throw new NotImplementedException();
        }
    }
}
