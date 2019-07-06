using Project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Cart
    {
        public BookingViewModel Vehicle { get; set; }
        public int Quantity { get; set; }
        public Cart(BookingViewModel vehicle, int quantity)
        {
            Vehicle = vehicle;
            Quantity = quantity;
        }
    }
}