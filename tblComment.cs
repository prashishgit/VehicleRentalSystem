namespace Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblComment
    {
        [Key]
        public int CommentId { get; set; }

        [StringLength(7999)]
        public string Comments { get; set; }

        public DateTime? ThisDateTime { get; set; }

        public int? VehicleId { get; set; }

        public int? Rating { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public virtual tblItem tblItem { get; set; }
    }
}
