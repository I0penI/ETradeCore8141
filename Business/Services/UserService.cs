﻿using AppCore.Business.Services.Bases;
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
            throw new NotImplementedException();
        }

        public Result Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
