﻿#nullable disable
using System.ComponentModel.DataAnnotations;
using AppCore.Records.Bases;

namespace DataAccess.Entities
{
    public class Product : RecordBase
    {
        [Required]
        [StringLength (200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public double UnirPrice { get; set; }

        public int StockAmount { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int CategoryId { get; set; }



    }
}
