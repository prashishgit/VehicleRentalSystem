namespace Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblItem")]
    public partial class tblItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblItem()
        {
            tblBookings = new HashSet<tblBooking>();
            tblComments = new HashSet<tblComment>();
        }

        [Key]
        public int VehicleId { get; set; }

        public int? VehicleCategoryId { get; set; }

        [StringLength(50)]
        public string VehiclePrice { get; set; }

        [StringLength(500)]
        public string VehicleTitle { get; set; }

        [StringLength(50)]
        public string VehiclePhoto { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string VehicleStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBooking> tblBookings { get; set; }

        public virtual tblCategory tblCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblComment> tblComments { get; set; }
    }
}
