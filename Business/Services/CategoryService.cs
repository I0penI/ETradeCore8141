using AppCore.Business.Services.Bases;
using AppCore.Results.Bases;
using Business.Models;
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
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
           _categoryRepo.Dispose();
        }

        public IQueryable<CategoryModel> Query()
        {
            return _categoryRepo.Query().OrderBy(c => c.Name).Select(c => new CategoryModel()
            {
                Description = c.Description,
                Guid = c.Guid,
                Id = c.Id,
                Name = c.Name
            });
        }

        public Result Update(CategoryModel model)
        {
            throw new NotImplementedException();
        }
    }

}
