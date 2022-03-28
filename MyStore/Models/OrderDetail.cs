using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class OrderDetail
    {
        public OrderDetail()
        {

        }

        public OrderDetail(Product product)
        {
            ProductId = product.Id;
            Name = product.Name;
            Price = product.Price;
            BonusPercentage = product.BonusPercentage;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Tên Sản Phẩm")]
        public string Name { get; set; }

        public float Quanlity { get; set; }

        public float Price { get; set; }

        public float BonusPercentage { get; set; }

        public float BonusPoint { get; set; }

        public Product Product { get; set; }

        public string CustomerPhone { get; set; }

        internal static Task<string> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
