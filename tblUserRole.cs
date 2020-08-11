namespace Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblUserRole")]
    public partial class tblUserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        public int? RoleId { get; set; }

        public int? UserId { get; set; }

        public virtual tblRole tblRole { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
