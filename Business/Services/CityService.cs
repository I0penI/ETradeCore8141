using AppCore.Business.Services.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Repostitories;

namespace Business.Services
{
    public interface ICityService : IService<CityModel>
    {

    }
    public class CityService : ICityService
    {
        private readonly CityRepoBase _cityRepo;

        public CityService(CityRepoBase cityRepo)
        {
            _cityRepo = cityRepo;
        }

        public Result Add(CityModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _cityRepo.Dispose();
        }

        public IQueryable<CityModel> Query()
        {
            return _cityRepo.Query().OrderBy(c => c.Name).Select(c => new CityModel()
            {
                Name = c.Name,
                CountryId = c.CountryId,
                Id = c.Id,
                Guid = c.Guid

            });
        }

        public Result Update(CityModel model)
        {
            throw new NotImplementedException();
        }
    }
}
