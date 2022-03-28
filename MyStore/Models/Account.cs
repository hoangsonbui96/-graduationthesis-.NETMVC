using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class Account
    {
        [Key]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Fullname { get; set; }

        public string EmailAdress { get; set; }

        public string PhoneNumber { get; set; }

        public string RoleCode { get; set; }
    }
}
