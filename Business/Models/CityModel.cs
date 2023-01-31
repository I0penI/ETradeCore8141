#nullable disable 
using System.ComponentModel.DataAnnotations;
using AppCore.Records.Bases;

namespace Business.Models
{
    public class CityModel : RecordBase
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int CountryId { get; set; }
    }
}
