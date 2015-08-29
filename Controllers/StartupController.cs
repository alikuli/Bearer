using AliKuli;
using Bearer.DAL;
//using AppDbx.Models;
using Bearer.Models;
using Bearer.MyPrograms.SetupStrategy;
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
            //var startUpValue = db.SetUps.FirstOrDefault(x => x.Name == "ShowStartUpScreenOnStartup");
            //string startUpValue = new ShowStartUpScreenOnStartupStrategy(db, AliKuli.GetUser.Name).GetFromTable();
            string userName = GetSet.Name();
            string startupValue = new ShowStartUpScreenOnStartupStrategy(new SetUpDAL(db, userName), userName).ValueDb();
            if (startupValue.Trim().ToLower() == "yes")
                return RedirectToAction("Index", "SetUps");
            else
                return RedirectToAction("Index", "Home");

           
        }
    }
}