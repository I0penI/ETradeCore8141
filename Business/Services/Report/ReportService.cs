using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models.Report;
using DataAccess.Entities;
using DataAccess.Repostitories;

namespace Business.Services.Report
{
	public interface IReportService
	{
		List<ReportItemModel> GetReport(ReportFilterModel filter, bool useInnerJoin = false);
	}
	public class ReportService : IReportService
	{
		private readonly ProductRepoBase _repo;

		public ReportService(ProductRepoBase repo)
		{
			_repo = repo;
		}

		public List<ReportItemModel> GetReport(ReportFilterModel filter, bool useInnerJoin = false)
		{
			var productQuery = _repo.Query();
			var categoryQuery = _repo.Query<Category>();
			var storeQuery = _repo.Query<Store>();
			var productStoreQuery = _repo.Query<ProductStore>();

			IQueryable<ReportItemModel> query = null;

			if (useInnerJoin)
			{
				query = from p in productQuery
						join c in categoryQuery
						on p.CategoryId equals c.Id
						join ps in productStoreQuery
						on p.Id equals ps.ProductId
						join s in storeQuery
						on ps.StoreId equals s.Id
						//where p.Id == 5 || (p.StockAmount > 10 && p.StockAmount <= 20)
						//orderby s.Name descending, c.Name, p.Name descending
						select new ReportItemModel()
						{
							CategoryDescription = c.Description,
							CategoryName = c.Name,
							ExpirationDate = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy") : "",
							ProductDescription = p.Description,
							ProductName = p.Name,
							StockAmount = p.StockAmount + " pieces",
							StoreName = s.Name,
							UnitPrice = p.UnitPrice.ToString("C2"),

							CategoryId = c.Id
						};
				query = query.OrderBy(q => q.StoreName).ThenBy(query => query.CategoryName).ThenBy(q => q.ProductName);

				if (filter is not null)
				{
					if (filter.CategoryId.HasValue)					
						query = query.Where(q => q.CategoryId == filter.CategoryId);
					if (!string.IsNullOrWhiteSpace(filter.ProductName))
						query = query.Where(q => q.ProductName.ToLower().Contains(filter.ProductName.ToLower().Trim()));

					
				}
			}
			return query.ToList();
		}
	}
}
