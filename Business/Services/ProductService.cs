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
        Result DeleteImage(int id);
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
            //Product existingProduct = _repo.Query().SingleOrDefault(p => p.Name.ToLower() == model.Name.ToLower().Trim());
            //if (existingProduct is not null )
            //{
            //    return new ErrorResult("Product with same name exists!");
            //}
            // 2. Yöntem
            //if (_repo.Query().Any(p => p.Name.ToLower() == model.Name.ToLower().Trim()))
            if (_repo.Exists(p => p.Name.ToLower() == model.Name.Trim()))
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
                UnitPrice = model.UnitPrice.Value,

                ProductStores = model.StoreIds?.Select(sId => new ProductStore()
                {
                    StoreId = sId,
                }).ToList(),

                Image = model.Image,
                ImageExtension= model.ImageExtension?.ToLower()

            };
            _repo.Add(entity);
            model.Id = entity.Id;
            return new SuccessResult("Product added successfully.");
        }

        public Result Delete(int id)
        {
            _repo.Delete<ProductStore>(ps => ps.ProductId == id);

            _repo.Delete(id);
            //_repo.Delete(p => p.Id == id);
            return new SuccessResult("Protuct deleted successfully.");
        }

		public Result DeleteImage(int id)
		{
			var entity = _repo.Query(p => p.Id == id).SingleOrDefault();
            entity.Image = null;
            entity.ImageExtension = null;
            _repo.Update(entity);
            return new SuccessResult("Product Image Deleted Successfully.");
		}

		public void Dispose()
        {
            _repo.Dispose();
        }

        public IQueryable<ProductModel> Query()
        {
            return _repo.Query(p => p.Category, p => p.ProductStores).Select(p => new ProductModel()
            { // AutoMapper
                CategoryId = p.CategoryId,
                Description = p.Description,
                ExpirationDate = p.ExpirationDate,
                Guid = p.Guid,
                Id = p.Id,
                Name = p.Name,
                StockAmount = p.StockAmount,
                UnitPrice = p.UnitPrice,
                UnitPriceDisplay = p.UnitPrice.ToString("C2"), //tr-TR
                ExpirationDateDisplay = p.ExpirationDate != null ? p.ExpirationDate.Value.ToString("MM/dd/yyyy") : "",
                //2.yol
                //ExpirationDateDisplay = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : "",

                CategoryNameDisplay = p.Category.Name,

                StoreIds = p.ProductStores.Select(ps => ps.StoreId).ToList(),

                StoreNamesDisplay = string.Join("<br />", p.ProductStores.Select(ps => ps.Store.Name)),

                Image = p.Image,
                ImageExtension = p.ImageExtension,

                ImgSrcDisplay = p.Image != null ? 
                    (
                        p.ImageExtension == ".jpg" || p.ImageExtension == ".jpeg" ? "data:image/jpeg;base64," : "data:image/png;base64,"   
                    ) + Convert.ToBase64String(p.Image) : null

            });

        }

        public Result Update(ProductModel model)
        {
            if (_repo.Exists(p => p.Name.ToLower() == model.Name.ToLower().Trim() && p.Id != model.Id))
                return new ErrorResult("Product with same name exists!");


            _repo.Delete<ProductStore>(ps => ps.ProductId == model.Id);

            Product entity = _repo.Query().SingleOrDefault(p => p.Id == model.Id);

            entity.CategoryId = model.CategoryId.Value;
            entity.Description = model.Description?.Trim();
            entity.ExpirationDate = model.ExpirationDate;
            entity.Name = model.Name.Trim();
				entity.StockAmount = model.StockAmount.Value;
            entity.UnitPrice = model.UnitPrice.Value;

            entity.ProductStores = model.StoreIds?.Select(sId => new ProductStore()
            {
                StoreId = sId
            }).ToList();
                
			

            if (model.Image != null)
            {
                entity.Image = model.Image;
                entity.ImageExtension = model.ImageExtension.ToLower();
            }
           

            _repo.Update(entity); 
            return new SuccessResult("Product updated successfully");
		}
    }
}
