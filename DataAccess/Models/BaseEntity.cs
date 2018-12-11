using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
    public class BaseEntity
    {
        [Column( Order = 220)]
        [StringLength(100)]
        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }
        [Column( Order = 221)]
        [DefaultValue("getdate()")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }
        [Column( Order = 222)]
        [StringLength(100)]
        [Display(Name = "ModifiedBy")]
        public string ModifiedBy { get; set; }
        [Column( Order = 223)]
        [DefaultValue("getdate()")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }
        [Column( Order = 224)]
        [DefaultValue("1")]
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

    }
}
