using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class Customer : BaseDataModel
    {
        public Customer()
        {

        }
        public int Id { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerName { get; set; }

        public string ShippingAddress { get; set; }

        public string RoleCode { get; set; }
    }
}
