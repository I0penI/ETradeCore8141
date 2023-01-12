using System.Globalization;
using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
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
            // 1. Yöntem
            //Product existingProduct = _repo.Query().SingleOrDefault(p => p.Name.ToUpper() == model.Name.ToUpper().Trim());
            //if (existingProduct is not null )
            //{
            //    return new ErrorResult("Product with same name exists!");
            //}
            // 2. Yöntem
            //if (_repo.Query().Any(p => p.Name.ToUpper() == model.Name.ToUpper().Trim()))
            if (_repo.Exists(p => p.Name.ToUpper() == model.Name.Trim()))
                return new ErrorResult("Product with same name exists!");

            if (model.ExpirationDate.HasValue && model.ExpirationDate.Value <= DateTime.Today)
            {
                return new ErrorResult("Expiration date must be after today!");
            }
            
            
            Product entity = new Product()
            {
                //CategoryId = model.CategoryId.HasValue ? model.CategoryId.Value : 0,
                //CategoryId = model.CategoryId ?? 0,
                CategoryId = model.CategoryId.Value,
                //Description = string.IsNullOrWhiteSpace(model.Description) ? null : model.Description.Trim(),
                Description = model.Description?.Trim(),
                ExpirationDate = model.ExpirationDate,
                Name = model.Name.Trim(),
                StockAmount = model.StockAmount.Value,
                UnitPrice = model.UnitPrice.Value
            };
            _repo.Add(entity);
            return new SuccessResult("Product added successfully.");
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
