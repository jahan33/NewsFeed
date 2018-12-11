using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
   public class tblChannelSubscribe:BaseEntity
    {
        [Key]
        public int PKUserSubscribe { get; set; }
        public int? FKChannel { get; set; }
        public int? FKUser { get; set; }
      
        [ForeignKey("FKChannel")]
        public virtual tblChannel tblChannel { get; set; }
       
        [ForeignKey("FKUser")]
        public virtual tblUser tblUser { get; set; }
    }
}
