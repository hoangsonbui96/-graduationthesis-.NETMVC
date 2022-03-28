using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class OrderHeader : BaseDataModel
    {
        public int Id { get; set; }

        [Display(Name = "Customer Phone Number ")]
        public string CustomerPhoneNumber { get; set; }

        [Display(Name="Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Display(Name = "Order Discount")]
        public float OrderDiscount { get; set; }

        public DateTime SubmitedTime { get; set; }
        public DateTime PerchasedTime { get; set; }
        public DateTime ProcessedTime { get; set; }
        public DateTime DeliveredTime { get; set; }
        public DateTime CompletedTime { get; set; }
        public DateTime CanceledTime { get; set; }



    }
}
