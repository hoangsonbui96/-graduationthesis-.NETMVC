using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class BaseDataModel
    {
        [Display(Name="Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Time")]
        public DateTime? CreatedTime { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated Time")]
        public DateTime? UpdatedTime { get; set; }

        public void Update ( string updator)
        {
            UpdatedBy = updator;
            UpdatedTime = DateTime.Now;
        }
        public void Initial(string creator)
        {
            CreatedBy = creator;
            UpdatedBy = creator;
            CreatedTime = UpdatedTime = DateTime.Now;
        }

    }
}
