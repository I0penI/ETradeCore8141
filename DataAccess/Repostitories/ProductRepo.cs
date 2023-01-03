using AppCore.DataAccess.EntitiyFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Repostitories
{
    public abstract class ProductRepoBase : RepoBase<Product>
    {
        protected ProductRepoBase(ETradeContext dbContext) : base(dbContext)
        {
        }
    }

    public class ProductRepo : ProductRepoBase
    {
        public ProductRepo(ETradeContext dbContext) : base(dbContext)
        {
        }
    }
}
