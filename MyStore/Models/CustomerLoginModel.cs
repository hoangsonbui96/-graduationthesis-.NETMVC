﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class CustomerLoginModel
    {
        [Required]
        public string CustomerPhoneNumber { get; set; }
    }
}
