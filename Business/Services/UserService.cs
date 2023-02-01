using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repostitories;

namespace Business.Services
{
    public interface IUserService : IService<UserModel>
    {

    }
    public class UserService : IUserService
    {
        private readonly UserRepoBase _userRepo;

        public UserService(UserRepoBase userRepo)
        {
            _userRepo = userRepo;
        }

        public Result Add(UserModel model)
        {
            if (_userRepo.Exists(u => u.UserName.ToLower() == model.UserName.ToLower().Trim()))
                return new ErrorResult("The user with same name exists!");
            User entity = new User()
            {
                UserName = model.UserName,
                Password = model.Password,
                IsActive = model.IsActive,
                RoleId = model.RoleId,
                UserDetail = new UserDetail()
                {
					Address = model.UserDetail.Address.Trim(),
					Email = model.UserDetail.Email.Trim(),
					Sex = model.UserDetail.Sex,
					CityId = model.UserDetail.CityId ?? 0,
					CountryId = model.UserDetail.CountryId.Value
                    
				}
                
            };
            _userRepo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _userRepo.Dispose();
        }

        public IQueryable<UserModel> Query()
        {
            return _userRepo.Query(u => u.Role).Select(u => new UserModel
            {
                Guid = u.Guid,
                UserName = u.UserName,
                Password = u.Password,
                IsActive = u.IsActive,
                RoleId = u.RoleId,
                Id = u.Id,
                RoleName = u.Role.Name
                
            });
        }

        public Result Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
