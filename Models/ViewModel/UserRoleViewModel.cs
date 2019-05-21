using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models.ViewModel
{
    public class UserRoleViewModel
    {
        public int UserRoleId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> UserId { get; set; }

        public virtual tblRole tblRole { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}