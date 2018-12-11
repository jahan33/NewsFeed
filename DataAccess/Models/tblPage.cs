using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
  public  class tblPage:BaseEntity
    {
        [Key]
        public int PKPage { get; set; }
        [Required(ErrorMessage = "Page name is required")]
        [StringLength(150)]
        public string PageName { get; set; }

        [Required(ErrorMessage = "Page URL is required")]
        [StringLength(250)]
        public string PageURL { get; set; }
    }
}
