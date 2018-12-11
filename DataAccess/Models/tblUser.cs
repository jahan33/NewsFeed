using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
   public class tblUser:BaseEntity
    {
        public tblUser()
        {
            tblUserRole = new HashSet<tblUserRole>();
            tblNewsSubscribe = new HashSet<tblNewsSubscribe>();
        }
        [Key]
        public int PKUser { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(150)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(150)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(250)]
        public string Password { get; set; }
        public int InvalidAttempt { get; set; }
        [DefaultValue("0")]
        public bool IsBlocked { get; set; }
        [DefaultValue("0")]
        public bool IsChangePassword { get; set; }
        public ICollection<tblUserRole> tblUserRole { get; set; }
        public ICollection<tblNewsSubscribe> tblNewsSubscribe { get; set; }
    }
}
