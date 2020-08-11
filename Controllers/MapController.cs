using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class MapController : Controller
    {
        private Pusher pusher;

        public MapController()
        {

            var options = new PusherOptions();
            options.Cluster = "ap2";

            pusher = new Pusher(
           "831202",
           "363fa98d64774712397e",
           "d7deaf4c8d451d3d05f8",
           options);
        }
       public ActionResult Index_Map()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index()
        {
            var latitude = Request.Form["lat"];
            var longitude = Request.Form["lng"];

            var location = new
            {
                latitude = latitude,
                longitude = longitude
            };

            pusher.TriggerAsync("location_channel", "new_location", location);

            return Json(new { status = "success", data = location });
        }
    }
}