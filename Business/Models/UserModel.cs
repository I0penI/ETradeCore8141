﻿#nullable disable
using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Business.Models
{
    public class UserModel : RecordBase
    {
		#region Entity
		[Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }
        #endregion

        public string RoleName { get; set; }

    }
}
