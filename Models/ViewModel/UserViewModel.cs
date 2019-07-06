using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Models.ViewModel
{
    [MetadataType(typeof(UserViewModel))]
   
    public class UserViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserViewModel()
        {
            this.tblUserRoles = new HashSet<tblUserRole>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        [Remote("IsUserNameAvailable", "User", ErrorMessage ="UserName already in use")]
        [Required]
        public string UserName { get; set; }

       
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FullName { get; set; }
        [Remote("IsEmailAvailable", "User", ErrorMessage = "Email already in use")]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Photo { get; set; }
        public string CitizenshipPhoto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserRole> tblUserRoles { get; set; }
    }
}