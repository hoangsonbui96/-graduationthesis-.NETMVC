using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class RolePermision : BaseDataModel
    {
        [Required]
        [StringLength(200)]
        public string RoleCode { get; set; }

        [Required]
        [StringLength(200)]
        public string FunctionRole { get; set; }

    }
}
