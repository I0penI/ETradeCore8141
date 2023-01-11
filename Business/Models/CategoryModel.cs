#nullable disable

using System.ComponentModel.DataAnnotations;
using AppCore.Records.Bases;

namespace Business.Models
{
    public class CategoryModel : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
