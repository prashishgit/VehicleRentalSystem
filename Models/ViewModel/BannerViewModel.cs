using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models.ViewModel
{
    public class BannerViewModel
    {
        public int BannerId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HeadingOne { get; set; }
        public string HeadingTwo { get; set; }
    }
}