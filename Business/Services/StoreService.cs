using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repostitories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business.Services
{
	public interface IStoreService : IService<StoreModel>
	{

	}
	public class StoreService : IStoreService
	{
		private readonly StoreRepoBase _storeRepo;

		public StoreService(StoreRepoBase storeRepo)
		{
			_storeRepo = storeRepo;
		}

		public Result Add(StoreModel model)
		{
			if (_storeRepo.Exists(s => s.Name.ToLower() == model.Name.ToLower().Trim() && s.IsVirtual == model.IsVirtual))
				return new ErrorResult("Store with same name exists!");
			Store entity = new Store()
			{
				IsVirtual = model.IsVirtual,
				Name = model.Name.Trim(),
			};
			_storeRepo.Add(entity);
			return new SuccessResult();
		}

		public Result Delete(int id)
		{
			_storeRepo.Delete(id);
			return new SuccessResult();
		}

		public void Dispose()
		{
			_storeRepo.Dispose();
		}

		public IQueryable<StoreModel> Query()
		{
			return _storeRepo.Query().OrderBy(s => s.IsVirtual).ThenBy(s => s.Name).Select(s => new StoreModel()
			{
				Name = s.Name,
				IsVirtual = s.IsVirtual,
				Guid = s.Guid,
				Id = s.Id,
				IsVirtualDisplay = s.IsVirtual ? "Yes" : "No"

			});
		}

		public Result Update(StoreModel model)
		{
			if (_storeRepo.Exists(s => s.Name.ToLower() == model.Name.ToLower().Trim() && s.IsVirtual == model.IsVirtual && s.Id != model.Id))
				return new ErrorResult("Store with same name exists!");
			Store entity = new Store()
			{
				Id= model.Id,
				IsVirtual = model.IsVirtual,
				Name = model.Name.Trim(),
			};
			_storeRepo.Update(entity);
			return new SuccessResult();
		}
	}
}
