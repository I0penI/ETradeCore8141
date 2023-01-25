#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Report
{
	public class ReportItemModel
	{
		#region Report
		[DisplayName("Product Name")]
		public string ProductName { get; set; }

		public string ProductDescription { get; set; }

		[DisplayName("Unit Price")]
		public string UnitPrice { get; set; }

		[DisplayName("Stock Amount")]
		public string StockAmount { get; set; }

		[DisplayName("Expiration Date")]
		public string ExpirationDate { get; set; }

		[DisplayName("Category")]
		public string CategoryName { get; set; }

		public string CategoryDescription { get; set; }

		[DisplayName("Store")]
		public string StoreName { get; set; }
		#endregion

		#region Filters
		public int? CategoryId { get; set; }
		#endregion
	}

}
