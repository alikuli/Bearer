
using Bearer.DAL;
using Bearer.Models;
//using AppDbx.Models;
using Bearer.MyPrograms;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Bearer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            string role= AliKuli.MyConstants.AdminConst;
            
            IdentityRole roleIn;
            if (!roleManager.RoleExists(role))
            {
                roleIn = new IdentityRole { Name = role };
                roleManager.Create(roleIn);
            }
            
            var userStore = new UserStore<User>(db);
            var userManager = new UserManager<User>(userStore);

                       
            string userName = ConfigurationManager.AppSettings["AdminEmailAndUserName"];
            string adminPassword = ConfigurationManager.AppSettings["AdminPassword"];

            var userFound = userManager.FindByName(userName);
            
            if (userFound==null)
            {
                var user = new User { UserName = userName, PhoneNumber = userName, PhoneNumberConfirmed=true, CreatedDate=new DateTimeAdapter().UtcNow,CreatedUser="Auto Created", Active=true,Comment="This is the Administrator user and is created automatically by the Computer.",LockoutEnabled=true };
                userManager.Create(user, adminPassword);
                userManager.AddToRole(user.Id, role);
                
            }

            try
            {
                new SetUpDAL(db, userName).InitializeSetUp();
            }
            catch
            {

            }

            //SetupInitialize setItUp = new SetupInitialize(db, "System");
            //setItUp.Initialize();
            //setItUp.LoadIntoMemory();

            ////LOAD ALL THE VARIABLES INTO APPLICTION STATE
            //Application["CompanyName"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "CompanyName").Value.ToString();

            //Application["SendGridPassword"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "SendGridPassword").Value.ToString();

            //Application["SendGridUserName"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "SendGridUserName").Value.ToString();

            //Application["ShowStartUpScreenOnStartup"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "ShowStartUpScreenOnStartup").Value.ToString();

            //Application["DefaultPageSize"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "DefaultPageSize").Value.ToString();

            //Application["SmtpServer"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "SmtpServer").Value.ToString();

            //Application["SmtpUser"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "SmtpUser").Value.ToString();

            ////This is the smptp password
            //Application["SmtpPassword"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "SmtpPassword").Value.ToString();

            ////This controls if BCC are sent
            //Application["IsSendBcc"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "IsSendBcc").Value.ToString();

            ////This is where all the BCC emails go
            //Application["BccEmailAddress"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "BccEmailAddress").Value.ToString();

            ////This is the from email address used in the program
            //Application["FromEmailAddress"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "FromEmailAddress").Value.ToString();

            ////This is the website URL for the current website
            //Application["WebsiteUrl"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "WebsiteUrl").Value.ToString();

            ////This decides if sendgrid is used or a smtp
            //Application["UseSendgridOrSmtp"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "UseSendgridOrSmtp").Value.ToString();

            ////This decides if sendgrid is used or a smtp
            //Application["SmtpPort"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "SmtpPort").Value.ToString();

            ////This is the smtp domain
            //Application["SmtpDomain"] = db.SetUps
            //    .FirstOrDefault(x => x.Name == "SmtpDomain").Value.ToString();

        }
        //protected void AddToSetup(SetUp s)
        //{
        //    var itemExists = db.SetUps.FirstOrDefault(x => x.Name == s.Name && x.Type == s.Type);

        //    if (itemExists == null)
        //    {
        //        db.SetUps.Add(s);
        //        db.SaveChanges();
        //    }

        //}

    }
}
