using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Models.ViewModel
{
    public class LoginViewModel
    {
        
        [Required]
        public string UserName { get; set; }
       
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class RetriveViewModel
    {
        [Remote("EmailExists", "User", ErrorMessage = "Email does not exists")]
        [Required]
        public string Email { get; set; }
    }
}