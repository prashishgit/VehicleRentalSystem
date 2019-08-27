using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models.ViewModel
{
    public class ItemViewModel
    {
        public int VehicleId { get; set; }
        public int SN { get; set; }
        [Required]
        public Nullable<int> VehicleCategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public string VehiclePrice { get; set; }
        [Required]
        public string VehicleTitle { get; set; }
        [Required]
        public string VehiclePhoto { get; set; }
        [Required]
        public string Description { get; set; }
        public string VehicleStatus { get; set; }
        [Required]
        public DateTime PickUpDate { get; set; }
        [Required]
        public DateTime DropOffDate { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
       
        public virtual tblCategory tblCategory { get; set; }
    }
}