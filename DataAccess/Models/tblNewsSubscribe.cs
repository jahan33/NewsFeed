using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
   public class tblNewsSubscribe:BaseEntity
    {
        [Key]
        public int PKNewsSubscribe { get; set; }
        public int? FKNews { get; set; }
        public int? FKUser { get; set; }
      
        [ForeignKey("FKNews")]
        public virtual tblNews tblNews { get; set; }
       
        [ForeignKey("FKUser")]
        public virtual tblUser tblUser { get; set; }
    }
}
