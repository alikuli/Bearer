//using AppDbx.Models;
using Bearer.Models;
using Bearer.MyPrograms;
using Bearer.DAL;
using ModelsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bearer.Controllers

{
    public class SetUpsController : BaseController
    {
        private ApplicationDbContext db;
        private GlobalValuesVM globalValues;
        private SetUpDAL repo;
        private string userName=string.Empty;

        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// This is the constructor. All the variables are initialized here.
        /// </summary>
        public SetUpsController()
        {
            db = new ApplicationDbContext();
            globalValues = new GlobalValuesVM(db);
            userName = AliKuli.GetUser.Name(User);
            repo = new SetUpDAL(db, userName);
        }

        //------------------------------------------------------------------------------------------------------------

        // GET: SetUps
        public ActionResult Index(string message)
        {
            //This handles the messages
            //Add a message if the message is not empty
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("",message);
            }


            var setupList = repo.FindAll().OrderBy(x => x.Description).ToList();
            
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


        //------------------------------------------------------------------------------------------------------------

        // GET: SetUps/Details/5
        public ActionResult Details(long? id)
        {

            SetUp setUp= new SetUp();

            try
            {
                setUp = repo.FindFor(long.Parse(id.ToString()));
                return View(setUp);

            }

            catch(Exception e)
            {
                string message = e.Message;
                
                if (e.InnerException != null)
                    message += " SYSTEM ERROR: " + e.InnerException.Message;

                return RedirectToIndexActionErrorHelper("There was a problem. No record was found. ", e);


            }


        }


        //------------------------------------------------------------------------------------------------------------


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
                try
                {
                    repo.Create(setUp);
                    repo.Save();
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", "Error getting Edit record: " + e.Message);
                    if (e.InnerException != null)
                        ModelState.AddModelError("", "SYSTEM: " + e.InnerException.Message);
                    
                }
                return RedirectToIndexActionHelper("Your Record has been saved!");
            }
            ModelState.AddModelError("", "The model had errors. Try again." );
            return View(setUp);
        }


        //------------------------------------------------------------------------------------------------------------


        // GET: SetUps/Edit/5
        public ActionResult Edit(long? id)
        {
            
            SetUp setUp = new SetUp();

            try
            {
                setUp = repo.FindFor(long.Parse(id.ToString()));

            }
            catch(Exception e)
            {
                return RedirectToIndexActionErrorHelper("There was a problem. No record was found. ", e);
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


        //------------------------------------------------------------------------------------------------------------


        // POST: SetUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Value")] SetupVM setUp)
        {

            if (ModelState.IsValid)
            {
                SetUp sDb = new SetUp();
                try
                {
                    sDb = repo.FindFor(setUp.Id);
                }
                catch (Exception e)
                
                {
                    return RedirectToIndexActionErrorHelper("There was a problem. No record was found. ", e);

                }


                sDb.Value = setUp.Value;
                setUp.Description = sDb.Description;


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
                    string message = string.Empty;

                    try
                    {
                        switch (incomingEmailAndUrl)
                        {
                            case "FromEmailAddress":
                                if (!AliKuli.Validators.MyValidators.IsValidEmail(sDb.Value))
                                {

                                    message = "The FROM Email is not valid!";
                                    throw new Exception(message);

                                }
                                break;

                            case "BccEmailAddress":
                                if (!AliKuli.Validators.MyValidators.IsValidEmail(sDb.Value))
                                {
                                    message = "The BCC Email is not valid!";
                                    throw new Exception(message);

                                }
                                break;

                            case "WebsiteUrl":
                                if (!AliKuli.Validators.MyValidators.IsValidUrl(sDb.Value))
                                {
                                    message = "The URL is not valid!";
                                    throw new Exception(message);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    catch(Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                        return View(setUp);

                    }

                
                }

                sDb.ModifiedDate = DateTime.UtcNow;
                sDb.ModifiedUser = userName;


                try
                {
                    repo.Update(sDb);
                    db.SaveChanges();


                }

                catch(Exception e)
                {
                    ModelState.AddModelError("", "Your answer was not saved. Try again! Exception: " + e.Message);

                    if (e.InnerException != null)
                        ModelState.AddModelError("", "SYSTEM: " + e.InnerException.Message);

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
                            throw new Exception(error);
                    }
                    return RedirectToIndexActionHelper("Record Saved!");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View(setUp);
                }

            }
            return View(setUp);
        }


        //------------------------------------------------------------------------------------------------------------


        // GET: SetUps/Delete/5
        public ActionResult Delete(int? id)
        {

            try
            {
                var setup = repo.FindFor(long.Parse(id.ToString()));
                return View(setup);
            }

            catch (Exception e)
            {
                return RedirectToIndexActionErrorHelper("There was a problem.", e);

            }

        }

        
        //------------------------------------------------------------------------------------------------------------

        
        
        // POST: SetUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                repo.Delete(id);
                return ResetHttp();
            }
            catch(Exception e)
            {

                return RedirectToIndexActionErrorHelper("There was a problem. The Record was not Reset.", e);

            }

        }


        //------------------------------------------------------------------------------------------------------------

        public ActionResult Reset()
        {
            return ResetHttp();
        }



        //------------------------------------------------------------------------------------------------------------

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
            SetupSetup setup = new SetupSetup(db,userName);
            try
            {
                setup.Initialize();
                setup.LoadIntoMemory();

            }
            catch (Exception e)
            {

                return RedirectToIndexActionErrorHelper("There was a problem. The Record was not Reset", e);

            }
            return RedirectToIndexActionHelper("There has now been a RESET. Now, you will need to add the values for each of the fields again!");
        }



        //------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// This resets the current field to its orignal value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ResetField(int id)
        {
            if (id == 0)
                return RedirectToIndexActionHelper("Please Try Again. I did not receive any record.");

            return ResetFieldHttp(id);
        }



        //------------------------------------------------------------------------------------------------------------

        public ActionResult ResetFieldHttp(int id)
        {
            var s = db.SetUps.FirstOrDefault(x => x.Id == id);
            if (s==null)
                return RedirectToIndexActionHelper("Please Try Again. A Null value was received");

            try
            {
                repo.Delete(s);
                repo.Save();
            }
            catch(Exception e)
            {
                return RedirectToIndexActionErrorHelper("There was a problem. The Record was not deleted.", e);


            }
            //add back
            SetupSetup setup = new SetupSetup(db, userName);
            
            try
            {
                setup.Initialize(s.Name);
                setup.LoadIntoMemory();
                return RedirectToIndexActionHelper(string.Format("Field '{0}' Has been Reset",s.Description));
            }

            catch(Exception e)
            {
                string message = e.Message;

                if (e.InnerException != null)
                    message += " SYSTEM ERROR: " + e.InnerException.Message;

                return RedirectToIndexActionErrorHelper("There was a problem. The Record was not Reset",e);
            }
        }


        //------------------------------------------------------------------------------------------------------------


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
