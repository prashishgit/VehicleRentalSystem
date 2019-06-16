using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Models.ViewModel
{
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

        public string UserName { get; set; }


        public string Password { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserRole> tblUserRoles { get; set; }
    }
}