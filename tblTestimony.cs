namespace Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblTestimony")]
    public partial class tblTestimony
    {
        [Key]
        public int TestimonyId { get; set; }

        public int? UserId { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(50)]
        public string TestimonyDescription { get; set; }
    }
}
