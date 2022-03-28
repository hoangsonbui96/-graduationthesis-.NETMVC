using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class RoleMaster : BaseDataModel
    {
        [Key]
        [StringLength(200)]
        [Required]
        public string RoleCode { get; set; }

        public string RoleName { get; set; }

    }
}
