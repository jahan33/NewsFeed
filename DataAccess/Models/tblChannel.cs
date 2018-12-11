using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
   public class tblChannel:BaseEntity
    {
        public tblChannel()
        {
            tblNews = new HashSet<tblNews>();
            tblChannelSubscribe = new HashSet<tblChannelSubscribe>();
            
        }
        [Key]        
        public int PKChannel { get; set; }
        public int FKChannelType { get; set; }
        public int FKOwner { get; set; }

        [Column("ChannelName", Order = 1)]
        [Required(ErrorMessage = "Channel name is required")]
        [StringLength(150)]
        public string ChannelName { get; set; }


        [Column("ChannelDescription", Order = 2)]
        [StringLength(250)]
        public string ChannelDescription { get; set; }



        [ForeignKey("FKOwner")]
        [Column("FKOwner", Order = 3)]
        public virtual tblUser tblUser { get; set; }


        
        [ForeignKey("FKChannelType")]
        [Column("FKChannelType", Order = 4)]
        public virtual tblChannelType tblChannelType { get; set; }
        public ICollection<tblNews> tblNews { get; set; }
        public ICollection<tblChannelSubscribe> tblChannelSubscribe { get; set; }

    }
}
