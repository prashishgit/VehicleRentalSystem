﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models.ViewModel
{
    public class ItemViewModel
    {
        public int VehicleId { get; set; }
        public int SN { get; set; }
        public Nullable<int> VehicleCategoryId { get; set; }
        public string VehiclePrice { get; set; }
        public string VehicleTitle { get; set; }
        public string VehiclePhoto { get; set; }
        public string Description { get; set; }
        public string VehicleStatus { get; set; }

        public virtual tblCategory tblCategory { get; set; }
    }
}