using AppCore.DataAccess.EntitiyFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repostitories
{
    public abstract class UserRepoBase : RepoBase<User>
    {
        protected UserRepoBase(ETradeContext dbContext) : base(dbContext)
        {
        }
    }

    public class UserRepo : UserRepoBase
    {
        public UserRepo(ETradeContext dbContext) : base(dbContext)
        {
        }
    }
}
