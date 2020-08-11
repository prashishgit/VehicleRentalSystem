namespace Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblBanner")]
    public partial class tblBanner
    {
        [Key]
        public int BannerId { get; set; }

        [StringLength(50)]
        public string Photo { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string HeadingOne { get; set; }

        [StringLength(50)]
        public string HeadingTwo { get; set; }
    }
}
