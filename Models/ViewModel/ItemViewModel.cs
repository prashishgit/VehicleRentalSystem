using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models.ViewModel
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string ItemPrice { get; set; }
        public string ItemTitle { get; set; }
        public string ItemPhoto { get; set; }
        public string Description { get; set; }
        public string ItemStatus { get; set; }
    }
}