using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
  public  class tblNews:BaseEntity
    {
        public tblNews()
        {
            tblNewsSubscribe = new HashSet<tblNewsSubscribe>();
        }
        [Key]
        public int PKNews { get; set; }
        public int FKChannel { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(250)]
        public string Author { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }
        [StringLength(4000)]
        public string Content { get; set; }


       
        [StringLength(250)]
        public string ImageURL { get; set; }
        [ForeignKey("FKChannel")]
        public virtual tblChannel tblChannel { get; set; }
        public ICollection<tblNewsSubscribe> tblNewsSubscribe { get; set; }


    }
}
