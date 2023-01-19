using System.Linq.Expressions;
using AppCore.DataAccess.EntitiyFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Repostitories
{
	public abstract class StoreRepoBase : RepoBase<Store>
	{
		public StoreRepoBase(ETradeContext dbContext) : base(dbContext)
		{
		}

		public override IQueryable<Store> Query(params Expression<Func<Store, object>>[] entitiesToInclude)
		{
			return base.Query(entitiesToInclude).Where(q => !q.IsDeleted);
		}

		public override void Delete(Store entity, bool save = true)
		{
			entity.IsDeleted = true; 
			base.Update(entity, save);
		}
	}
	public class StoreRepo : StoreRepoBase
	{
		public StoreRepo(ETradeContext dbContext) : base(dbContext)
		{
		}
	}
}
