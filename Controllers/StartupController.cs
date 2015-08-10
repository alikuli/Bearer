using Bearer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bearer.Controllers

{
    public class StartupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Startup
        public ActionResult Index()
        {
            var startUpValue = db.SetUps.FirstOrDefault(x => x.Name == "ShowStartUpScreenOnStartup");
            if (startUpValue.Value.Trim().ToLower() == "yes")
                return RedirectToAction("Index", "SetUps");
            else
                return RedirectToAction("Index", "Home");

           
        }
    }
}