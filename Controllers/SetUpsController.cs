using Bearer.Models;
using Bearer.MyPrograms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bearer.Controllers

{
    public class SetUpsController : Controller
    {
        private ApplicationDbContext db;
        private GlobalValuesVM globalValues;


        public SetUpsController()
        {
            db = new ApplicationDbContext();
            globalValues = new GlobalValuesVM(db);
        }


        // GET: SetUps
        public ActionResult Index(string messageType, string message)
        {
            if (!string.IsNullOrEmpty(messageType))
            {
                if (messageType.ToLower()=="resetmessage")
                {
                    ModelState.AddModelError("","The setup has been reset.");
                }

                if (messageType.ToLower()=="errormessage")
                {
                    ModelState.AddModelError("",message);
                }
            }
            var setupList = db.SetUps.OrderBy(x => x.Description).ToList();
            List<SetupVM> setupListVM = new List<SetupVM>();

            if (setupList != null)
            {
                foreach (var item in setupList)
                {
                    SetupVM sVM = new SetupVM();
                    sVM.Id = item.Id;
                    sVM.Description = item.Description;
                    sVM.Value = item.Value;
                    sVM.Name = item.Name;


                    string incomingName = item.Name;

                   switch (incomingName)
                   {
                       case "CompanyName":
                           sVM.ApplicationValue = globalValues.CompanyName;
                           break;
                       case "SendGridUserName":
                           sVM.ApplicationValue = globalValues.SendGridUserName;
                           break;
                       case "SendGridPassword":
                           sVM.ApplicationValue = globalValues.SendGridPassword;
                           break;
                       case "SmtpPassword":
                           sVM.ApplicationValue = globalValues.SmtpPassword;
                           break;
                       case "SmtpServer":
                           sVM.ApplicationValue = globalValues.SmtpServer;
                           break;
                       case "SmtpUser":
                           sVM.ApplicationValue = globalValues.SmtpUser;
                           break;
                       case "ShowStartUpScreenOnStartup":
                           sVM.ApplicationValue = globalValues.ShowStartUpScreenOnStartup;
                           break;
                       case "DefaultPageSize":
                           sVM.ApplicationValue = globalValues.DefaultPageSize;
                           break;
                       case "FromEmailAddress":
                           sVM.ApplicationValue = globalValues.FromEmailAddress;
                           break;
                       case "BccEmailAddress":
                           sVM.ApplicationValue = globalValues.BccEmailAddress;
                           break;
                       case "UseSendgridOrSmtp":
                           sVM.ApplicationValue = globalValues.UseSendgridOrSmtp;
                           break;
                       case "WebsiteUrl":
                           sVM.ApplicationValue = globalValues.WebsiteUrl;
                           break;
                       case "IsSendBcc":
                           sVM.ApplicationValue = globalValues.IsSendBcc;
                           break;
                       case "SmtpPort":
                           sVM.ApplicationValue = globalValues.SmtpPort ;
                           break;

                       case "SmtpDomain":
                           sVM.ApplicationValue = globalValues.SmtpDomain ;
                           break;



                       default:
                           sVM.ApplicationValue = "error: " + incomingName;
                           break;

                   }


                    
                   setupListVM.Add(sVM);
                }

            }
            return View(setupListVM);
        }

        // GET: SetUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetUp setUp = db.SetUps.Find(id);
            if (setUp == null)
            {
                return HttpNotFound();
            }
            return View(setUp);
        }

        // GET: SetUps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SetUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Type,Value")] SetUp setUp)
        {
            if (ModelState.IsValid)
            {
                //check to see if the item already exists... if it does, then fail
                var itemExists = db.SetUps.FirstOrDefault(x => x.Name == setUp.Name && x.Type == setUp.Type);

                if(itemExists!=null)
                {
                    ModelState.AddModelError("", "This setup already exists!");
                    return View(setUp);
                }

                db.SetUps.Add(setUp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(setUp);
        }

        // GET: SetUps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetUp setUp = db.SetUps.Find(id);
            if (setUp == null)
            {
                return HttpNotFound();
            }

            SetupVM setupVM = new SetupVM();
            setupVM.Id = setUp.Id;
            setupVM.Description = setUp.Description;
            setupVM.Value = setUp.Value;

            if (setUp.Type==EnumTypes.EmailingMethod)
            {
                List<SelectListVM> emailOptions = new List<SelectListVM>
                {
                   new SelectListVM { Id="sendgrid",Name="sendgrid"},
                   new SelectListVM {Id="smtp",Name="smtp"},
                   new SelectListVM {Id="test",Name=@"Create dir at c:\TestEmail"}
                };

                ViewBag.Value = new SelectList(emailOptions, "Id", "Name", setupVM.Value);
                return View("EditWithDropDown", setupVM);
            }
            return View(setupVM);
        }

        // POST: SetUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Value")] SetupVM setUp)
        {
            if (setUp==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var sDb = db.SetUps.FirstOrDefault(s => s.Id == setUp.Id);

                sDb.Value = setUp.Value;
                setUp.Description = sDb.Description;

                if (sDb == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //CHECK BOOLS

                if (sDb.Type == EnumTypes.boolean)
                {
                    if (sDb.Value.Trim().ToLower() == "yes" ||
                        sDb.Value.Trim().ToLower() == "true" ||
                        sDb.Value.Trim().ToLower() == "no" ||
                        sDb.Value.Trim().ToLower() == "false")

                    {
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your answer must be 'Yes' or 'No' or 'True' or 'False.' Try again!");
                        
                        setUp.Description = sDb.Description;
                        return View(setUp);

                    }
                    if (sDb.Value.Trim().ToLower() == "yes" || sDb.Value.Trim().ToLower() == "true") 
                    {
                        sDb.Value = "true";
                    }
                    if (sDb.Value.Trim().ToLower() == "no" ||
                        sDb.Value.Trim().ToLower() == "false")
                    {
                        sDb.Value = "false";
                    }
                }


                //CHECK INT


                if(sDb.Type==EnumTypes.Integer)
                {
                    int theInteger;
                    bool success = int.TryParse(sDb.Value, out theInteger);

                    if(!success)
                    {
                        ModelState.AddModelError("", "You must enter an integer (number) for this value. Try again!");
                        return View(setUp);
                    }

                }

                //Check string
                if(sDb.Type==EnumTypes.String)
                {
                    //Check the emails and URL
                    string incomingEmailAndUrl = sDb.Name;

                    switch (incomingEmailAndUrl)
                    {
                        case "FromEmailAddress":
                            if (!AliKuli.Validators.MyValidators.IsValidEmail(sDb.Value))
                            {
                                ModelState.AddModelError("", "The FROM Email is not valid!");
                                return View(setUp);
                            }
                            break;

                        case "BccEmailAddress":
                            if (!AliKuli.Validators.MyValidators.IsValidEmail(sDb.Value))
                            {
                                ModelState.AddModelError("", "The BCC Email is not valid!");
                                return View(setUp);
                            }
                            break;

                        case "WebsiteUrl":
                                if (!AliKuli.Validators.MyValidators.IsValidUrl(sDb.Value))
                                {
                                    ModelState.AddModelError("", "The URL is not valid!");
                                    return View(setUp);
                                }
                                break;
                        default:
                            break;
                    }

                
                }


                db.Entry(sDb).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();


                }

                catch(Exception e)
                {
                    ModelState.AddModelError("", "Your answer was not saved. Try again! Exception: " + e.Message);
                    return View(setUp);
                }


                try
                {
                    //Reading values into memory
                    string incomingName = sDb.Name;

                    switch (incomingName)
                    {
                        case "CompanyName":
                            globalValues.CompanyName = sDb.Value;
                            AliKuli.GlobalSetupValues.CompanyName = sDb.Value;
                            break;
                        case "SendGridUserName":
                            globalValues.SendGridUserName = sDb.Value;
                            break;
                        case "SendGridPassword":
                            globalValues.SendGridPassword = sDb.Value;
                            break;
                        case "SmtpPassword":
                            globalValues.SmtpPassword= sDb.Value;
                            break;
                        case "SmtpServer":
                            globalValues.SmtpServer= sDb.Value;
                            break;
                        case "SmtpUser":
                            globalValues.SmtpUser= sDb.Value;
                            break;
                        case "ShowStartUpScreenOnStartup":
                            globalValues.ShowStartUpScreenOnStartup = sDb.Value;
                            break;
                        case "DefaultPageSize":
                            globalValues.DefaultPageSize = sDb.Value;
                            break;
                        case "FromEmailAddress":
                            globalValues.FromEmailAddress = sDb.Value;
                            break;
                        case "BccEmailAddress":
                            globalValues.BccEmailAddress = sDb.Value;
                            break;
                        case "UseSendgridOrSmtp":
                            globalValues.UseSendgridOrSmtp = sDb.Value;
                            break;
                        case "WebsiteUrl":
                            globalValues.WebsiteUrl = sDb.Value;
                            break;
                        case "IsSendBcc":
                            globalValues.IsSendBcc = sDb.Value;
                            break;

                        case "SmtpPort":
                            globalValues.SmtpPort = sDb.Value;
                            break;

                        case "SmtpDomain":
                            globalValues.SmtpDomain = sDb.Value;
                            break;
                            
                            
                            
                        default:
                            string error="There was a problem updating the Application in switch statement. Please have administrator restart the application to see the update. The application may not work properly.";
                            ModelState.AddModelError("",error);
                            break;
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View(setUp);
                }

                return RedirectToAction("Index");
            }
            return View(setUp);
        }

        // GET: SetUps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetUp setUp = db.SetUps.Find(id);
            if (setUp == null)
            {
                return HttpNotFound();
            }
            return View(setUp);
        }

        // POST: SetUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SetUp setUp = db.SetUps.Find(id);
            db.SetUps.Remove(setUp);
            db.SaveChanges();
            return ResetHttp();
        }


        public ActionResult Reset()
        {
            return ResetHttp();
        }


        /// <summary>
        /// This replaces any deleted fields
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetHttp()
        {
            ////delete all the current.
            //db.SetUps.RemoveRange(db.SetUps);
            //db.SaveChanges();
            //add back
            SetupSetup setup = new SetupSetup(db);
            setup.Initialize();
            setup.LoadIntoMemory();

            return RedirectToAction("Index", new { messageType = "resetmessage" });
        }

        /// <summary>
        /// This resets the current field to its orignal value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ResetField(int id)
        {
            if (id == 0)
                return View("Index", new { messageType="PleaseTryAgain" });
            return ResetFieldHttp(id);
        }

        public ActionResult ResetFieldHttp(int id)
        {
            var s = db.SetUps.FirstOrDefault(x => x.Id == id);
            if (s==null)
                return View("Index", new { messageType = "PleaseTryAgain" });

            db.SetUps.Remove(s);
            db.SaveChanges();
            //add back
            SetupSetup setup = new SetupSetup(db);
            try
            {
                setup.Initialize(s.Name);
            }
            catch(Exception e)
            {
                //Error that no option in switch statemeoint found
                return RedirectToAction("Index", new { messageType = "resetmessage", errormessage=e.Message.ToString()});

            }
            setup.LoadIntoMemory();

            return RedirectToAction("Index", new { messageType = "resetmessage" });

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
