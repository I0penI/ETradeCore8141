using AppCore.DataAccess.EntitiyFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repostitories
{

    public abstract class CategoryRepoBase : RepoBase<Category>
    {
        protected CategoryRepoBase(ETradeContext dbContext) : base(dbContext)
        {
        }
    }
    public class CategoryRepo : CategoryRepoBase
    {
        public CategoryRepo(ETradeContext dbContext) : base(dbContext)
        {
        }
    }
}
