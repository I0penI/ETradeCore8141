using AppCore.DataAccess.EntitiyFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Repostitories
{
    public abstract class CountryRepoBase : RepoBase<Country>
    {
        protected CountryRepoBase(ETradeContext dbContext) : base(dbContext)
        {
        }
    }
    public class CountryRepo : CountryRepoBase
    {
        public CountryRepo(ETradeContext dbContext) : base(dbContext)
        {
        }
    }
}
