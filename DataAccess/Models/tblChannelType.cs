using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
   public class tblChannelType:BaseEntity
    {
        [Key]
        public int PKChannelType { get; set; }
        [Required(ErrorMessage = "Channel type is required")]
        [StringLength(150)]
        public string TypeName { get; set; }
    }
}
