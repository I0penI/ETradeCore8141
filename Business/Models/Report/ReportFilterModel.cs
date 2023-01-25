#nullable disable
#nullable disable 
using System.ComponentModel;

namespace Business.Models.Report
{
    public class ReportFilterModel
    {
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [DisplayName("Product")]
        public string ProductName { get; set; }
    }
}
