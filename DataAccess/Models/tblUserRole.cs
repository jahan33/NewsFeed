using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
  public  class tblUserRole:BaseEntity
    {
        [Key]
        public int PKUserRole { get; set; }
        public int? FKUser { get; set; }
        public int? FKRole { get; set; }
        [DefaultValue("0")]
        public bool IsDefault { get; set; }
        [ForeignKey("FKUser")]
        public virtual tblUser tblUser { get; set; }
      
        [ForeignKey("FKRole")]
        public virtual tblRole tblRole { get; set; }
    }
}
