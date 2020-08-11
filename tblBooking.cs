namespace Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblBooking")]
    public partial class tblBooking
    {
        [Key]
        public int BookingId { get; set; }

        public int? VehicleId { get; set; }

        public int? UserId { get; set; }

        [StringLength(50)]
        public string CitizenshipPhoto { get; set; }

        [StringLength(50)]
        public string PickUpDate { get; set; }

        [StringLength(50)]
        public string DropOffDate { get; set; }

        public int? TotalAmount { get; set; }

        public int? AmountPaid { get; set; }

        [StringLength(50)]
        public string BookingStatus { get; set; }

        public virtual tblItem tblItem { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
