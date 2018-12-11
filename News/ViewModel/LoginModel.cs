using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.ViewModel
{
    public class LoginModel
    {
        public string Username { get; set; }      
        public string Password { get; set; }
        public string IPAddress { get; set; }
        public string Origin { get; set; }
        public tblUser Users;
        public string ErrorMessage;
        public bool PasswordExpired { get; set; }
        public List<tblPage> menu;
    }
}
