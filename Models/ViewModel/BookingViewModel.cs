using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models.ViewModel
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public Nullable<int> VehicleId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string CitizenshipPhoto { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<int> TotalAmount { get; set; }
        public Nullable<int> AmountPaid { get; set; }
        public string BookingStatus { get; set; }

        public virtual tblItem tblItem { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}