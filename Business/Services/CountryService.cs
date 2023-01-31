
using AppCore.Business.Services.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Repostitories;

namespace Business.Services
{
    public interface ICountryService : IService<CountryModel>
    {
        List<CountryModel> GetList();
    }
    public class CountryService : ICountryService
    {
        private readonly CountryRepoBase _countryRepo;

        public CountryService(CountryRepoBase countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public Result Add(CountryModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _countryRepo.Dispose();
        }

        public List<CountryModel> GetList()
        {
            return Query().ToList();
        }

        public IQueryable<CountryModel> Query()
        {
            return _countryRepo.Query().OrderBy(c => c.Name).Select(c => new CountryModel()
            {
                Name = c.Name,
                Guid = c.Guid,
                Id = c.Id
            });
        }

        public Result Update(CountryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
