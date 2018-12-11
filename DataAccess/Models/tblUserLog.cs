using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
  public  class tblUserLog
  
    {
        [Key]
        public int PKUserLog { get; set; }
        public int? FKUser { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LoginTime { get; set; }
        public string Origin { get; set; }
        public string IPAddress { get; set; }
        public bool? LoginSuccess { get; set; }
        public bool? IsActive { get; set; }
       
    }
}
