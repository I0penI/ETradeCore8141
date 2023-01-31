using AppCore.DataAccess.EntitiyFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repostitories
{
    public class CityRepoBase : RepoBase<City>
    {
        public CityRepoBase(ETradeContext dbContext) : base(dbContext)
        {
        }
    }
    public class CityRepo : CityRepoBase
    {
        public CityRepo(ETradeContext dbContext) : base(dbContext)
        {
        }
    }
}
