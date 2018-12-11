using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
   public class tblRolePage:BaseEntity
    {
        [Key]
        public int PKRolePage { get; set; }
        public int FKPage { get; set; }
        public int FKRole { get; set; }

        [DefaultValue("0")]      
        public bool IsDefault { get; set; }
        [ForeignKey("FKPage")]
        public virtual tblPage tblPage { get; set; }

        [ForeignKey("FKRole")]
        public virtual tblRole tblRole { get; set; }
    }
}
