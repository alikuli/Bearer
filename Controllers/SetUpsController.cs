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
using ModelsClassLibrary.Models.Setup;
using Bearer.MyPrograms.SetupStrategy;
using AliKuli.Exceptions;

namespace Bearer.Controllers

{
    public class SetUpsController : BaseController
    {
        private ApplicationDbContext db;
        //private GlobalValuesVM globalValues;
        private SetUpDAL repo;
        private string userName=string.Empty;

        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// This is the constructor. All the variables are initialized here.
        /// </summary>
        public SetUpsController()
        {
            db = new ApplicationDbContext();
            userName = AliKuli.GetSet.Name(User);
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
                setUp = repo.FindFor(id);
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


        
        //Not Used 
        //POST: SetUps/Create
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
                    SetupContext setupContext = new SetupContext(repo, userName);
                    ISetupStrategy setupStrategy = setupContext.Create(setUp.Name);

                    repo.Create(setUp);
                    repo.Save();
                    setupStrategy.Memory = setUp.Name; 
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
                setUp = repo.FindFor(id);

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


                SetupContext setupContext = new SetupContext(repo, userName);
                ISetupStrategy setupStrategy = setupContext.Create(sDb.Name);
                
                sDb.Value = setUp.Value;
                try
                {
                    sDb.Value = setupStrategy.Validate(sDb);
                    repo.Update(sDb);
                    repo.Save();
                    setupStrategy.Memory = setUp.Value;
                }

                catch(Exception e)
                {
                    string m = MakeErrorMesage("Answer not saved.", e);
                    ModelState.AddModelError("", m);
                    
                    setUp.Description = sDb.Description;
                    setUp.Id = sDb.Id;
                    setUp.Name = sDb.Name;
                    setUp.Value = sDb.Value;

                    return View(setUp);
                }

            }
            return RedirectToIndexActionHelper("Saved!");
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

        //private IQueryable<SetUp> GetAllData()
        //{
        //    return repo.FindAll();
        //}


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

            try
            {
                repo.DeleteAll();
                try
                {
                    repo.InitializeSetUp();
                }
                catch(NoDuplicateException)
                {
                    //do nothing
                }
                //repo.Save();
            }
            catch(Exception e)
            {
                return RedirectToIndexActionErrorHelper("There was a problem. The initialization failed", e);

            }
            return RedirectToIndexActionHelper("Setup Initialized.");
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
            string theName = "";
            string fieldName = "";
            try
            {

                var s = repo.FindFor(id);
                if (s == null)
                    return RedirectToIndexActionHelper("Please Try Again. A Null value was received");
                //save the name because it will be deleted after this.
                theName = s.Name;
                fieldName=s.Description;

                repo.Delete(s);
                repo.Save();
            }
            catch(Exception e)
            {
                return RedirectToIndexActionErrorHelper("There was a problem. The Record was not deleted.", e);
            }

            ////add back
            //SetupInitialize setup = new SetupInitialize(db, userName);
            
            try
            {
                SetupSingle(theName);
                return RedirectToIndexActionHelper(string.Format("Field '{0}' Has been Reset",fieldName));
            }

            catch(Exception e)
            {
                return RedirectToIndexActionErrorHelper("There was a problem. The Record was not Reset",e);
            }
        }

        private void SetupSingle(string theName)
        {
            SetupContext setupContext = new SetupContext(repo, userName);
            ISetupStrategy strategy = setupContext.Create(theName);
            SetUp setupNew = new SetUp();
            setupNew = strategy.AddInfo(setupNew);
            repo.Create(setupNew);
            repo.Save();
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
//CHECK BOOLS
//if (sDb.Type == EnumTypes.Boolean)
//{
//    if (sDb.Value.Trim().ToLower() == "yes" ||
//        sDb.Value.Trim().ToLower() == "true" ||
//        sDb.Value.Trim().ToLower() == "no" ||
//        sDb.Value.Trim().ToLower() == "false")

//    {
//    }
//    else
//    {
//        ModelState.AddModelError("", "Your answer must be 'Yes' or 'No' or 'True' or 'False.' Try again!");

//        setUp.Description = sDb.Description;
//        return View(setUp);
//    }


//    if (sDb.Value.Trim().ToLower() == "yes" || sDb.Value.Trim().ToLower() == "true") 
//    {
//        sDb.Value = "true";
//    }

//    if (sDb.Value.Trim().ToLower() == "no" ||
//        sDb.Value.Trim().ToLower() == "false")
//    {
//        sDb.Value = "false";
//    }
//}



////CHECK INT
//if(sDb.Type==EnumTypes.Integer)
//{
//    int theInteger;
//    bool success = int.TryParse(sDb.Value, out theInteger);

//    if(!success)
//    {
//        ModelState.AddModelError("", "You must enter an integer (number) for this value. Try again!");
//        return View(setUp);
//    }

//}



//Check string
//if(sDb.Type==EnumTypes.String)
//{
//    //Check the emails and URL
//    string incomingEmailAndUrl = sDb.Name;
//    string message = string.Empty;

//    try
//    {
//        switch (incomingEmailAndUrl)
//        {
//            case "FromEmailAddress":
//                if (!AliKuli.Validators.MyValidators.IsValidEmail(sDb.Value))
//                {

//                    message = "The FROM Email is not valid!";
//                    throw new Exception(message);

//                }
//                break;

//            case "BccEmailAddress":
//                if (!AliKuli.Validators.MyValidators.IsValidEmail(sDb.Value))
//                {
//                    message = "The BCC Email is not valid!";
//                    throw new Exception(message);

//                }
//                break;

//            case "WebsiteUrl":
//                if (!AliKuli.Validators.MyValidators.IsValidUrl(sDb.Value))
//                {
//                    message = "The URL is not valid!";
//                    throw new Exception(message);
//                }
//                break;
//            default:
//                break;
//        }
//    }
//    catch(Exception e)
//    {
//        ModelState.AddModelError("", e.Message);
//        return View(setUp);

//    }

//}


//if(sDb.Type==EnumTypes.FilePath)
//{
//    //this is used for local testing only... should not work on the web.
//    try
//    {
//        if (!AliKuli.Validators.MyValidators.IsValidFilePath(sDb.Value))
//        {
//            string message = "The File Path is not valid!";
//            throw new Exception(message);
//        }
//    }
//    catch(Exception e)
//    {
//        ModelState.AddModelError("", e.Message);
//        return View(setUp);

//    }
//}

//sDb.ModifiedDate = DateTime.UtcNow;
//sDb.ModifiedUser = userName;
