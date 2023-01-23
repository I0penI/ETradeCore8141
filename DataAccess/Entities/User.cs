﻿#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DataAccess.Entities
{
    public class User : RecordBase
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
