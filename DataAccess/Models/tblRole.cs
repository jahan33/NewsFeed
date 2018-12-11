using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
  public  class tblRole:BaseEntity
    {
        [Key]
        public int PKRole { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        [StringLength(150)]
        public string RoleName { get; set; }
      
    }
}
