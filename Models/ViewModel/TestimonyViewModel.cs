using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models.ViewModel
{
    public class TestimonyViewModel
    {
        public int TestimonyId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Location { get; set; }
        public string TestimonyDescription { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}