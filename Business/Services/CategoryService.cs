using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repostitories;

namespace Business.Services
{
    public interface ICategoryService : IService<CategoryModel>
    {

    }
   
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepoBase _categoryRepo;

        public CategoryService(CategoryRepoBase categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public Result Add(CategoryModel model)
        {
            if (_categoryRepo.Exists(c => c.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Category with same name exists!");
            Category entity = new Category()
            {
                Name = model.Name.Trim(),
                Description = model.Description?.Trim(),
            };
            _categoryRepo.Add(entity);
            return new SuccessResult("Category added successfully.");
        }

        public Result Delete(int id)
        {
            CategoryModel existingCategory = Query().SingleOrDefault(c => c.Id == id);
            if (existingCategory == null)
            {
                return new ErrorResult("Category Not Found!");
            }
            if(existingCategory.ProductCountDisplay > 0)
            {
                return new ErrorResult("Category Cannot Be Deleted Because Category Contain Products!");
            }
            _categoryRepo.Delete(id);
            return new SuccessResult("Category Deleted Successfully.");
        }

        public void Dispose()
        {
           _categoryRepo.Dispose();
        }

        public IQueryable<CategoryModel> Query()
        {
            return _categoryRepo.Query(c => c.Products).OrderBy(c => c.Name).Select(c => new CategoryModel()
            {
                Description = c.Description,
                Guid = c.Guid,
                Id = c.Id,
                Name = c.Name,
                ProductCountDisplay = c.Products.Count
            });
        }

        public Result Update(CategoryModel model)
        {
            if(_categoryRepo.Exists(c => c.Name.ToLower() == model.Name.ToLower().Trim() && c.Id != model.Id ))
            
                return new ErrorResult("Category with same name exists");
                Category entity = new Category()
                {
                    Id = model.Id,
                    Name = model.Name.Trim(),
                    Description = model.Description?.Trim(),
                };
                _categoryRepo.Update(entity);
                return new SuccessResult("Category updated successfully.");
            
        }
    }

}
