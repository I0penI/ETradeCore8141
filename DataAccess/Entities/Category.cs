#nullable disable

using System.ComponentModel.DataAnnotations;
using AppCore.Records.Bases;

namespace DataAccess.Entities
{
    public class Category : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
