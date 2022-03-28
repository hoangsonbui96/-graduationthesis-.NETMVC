using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class Product : BaseDataModel
    {
        public int Id { get; set; }


        [Display(Name = "Tên Sản Phẩm")]
        public string Name { get; set; }

        [Display(Name = "Giá Sản Phẩm")]
        public float Price { get; set; }

        [Display(Name="% Bonus")]
        public float BonusPercentage { get; set; }

        [Display(Name="Còn")]
        public float RemainingStock { get; set; }
    }
}
