﻿#nullable disable

using System.ComponentModel.DataAnnotations;
using AppCore.Records.Bases;

namespace Business.Models
{
    public class ProductModel : RecordBase
    {
        #region Entity'den kopyalanan özellikler
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public double UnitPrice { get; set; }

        public int StockAmount { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int CategoryId { get; set; }
        #endregion

        #region Entitiy Dışındaki Özelleştirmeler
        public string UnitPriceDisplay { get; set; }

        public string ExpirationDateDisplay { get; set; }
        public string CategoryNameDisplay { get; set; }
        #endregion
    }
}
