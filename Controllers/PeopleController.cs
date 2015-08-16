using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.DAL;
using ModelsClassLibrary.Models.People;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bearer.Controllers
{
    public class PeopleController : BaseController
    {
        private ApplicationDbContext db;
        private IRepositry<Person> repo;
        private string userName=string.Empty;

        
        
        public PeopleController()
        {
            
            db = new ApplicationDbContext();
            userName = AliKuli.GetUser.Name(User);
            repo = new PersonDAL(db, userName);

        }


        //--------------------------------------------------------------------------------------------------------------

        // GET: People
        public async Task<ActionResult> Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("", message);
            }
            return View(await repo.FindAllAsync());
        }


        //--------------------------------------------------------------------------------------------------------------



        // GET: People/Details/5
        public async Task<ActionResult> Details(long? id)
        {

            try
            {
                var person = await repo.FindForAsync(id);
                return View(person);
            }
            catch(Exception e)
            {
                return RedirectToIndexActionErrorHelper("There was an error.", e);
            }
        }

        
        //--------------------------------------------------------------------------------------------------------------

        // GET: People/Create
        public ActionResult Create()
        {
            Person p = new Person();

            //this is when we start to create the record
            p.CreatedDateStarted = DateTime.UtcNow;
            return View(p);
        }




        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdentificationNo,FName,MName,LName,Sex,SonOfOrWifeOf,NameOfFatherOrHusband,HouseNo,Road,Address2,City,State,Zip,Country,TaxType,GeoLat,GeoLong,CreatedDateStarted,Comment")] Person person)
        //Id,BlackListed,CreatedDate,CreatedUser,ModifiedDateStart,ModifiedDate,ModifiedUser,Active,Deleted,DeletedByUser,DeleteDate
        {
            if (ModelState.IsValid)
            {

                try
                {
                    repo.Create(person);
                    await repo.SaveAsync();
                    return RedirectToIndexActionHelper(string.Format("The person '{0}' has been saved", person.FullName));

                }
                catch(Exception e)
                {
                    string message = MakeErrorMesage("Unable to create.", e);
                    ModelState.AddModelError("", message);
                }
            }
            ModelState.AddModelError("",string.Format("There was an error. The Person '{0}' did not get saved.",person.FullName));
            return View(person);
        }







        // GET: People/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            try
            {
                Person person = await repo.FindForAsync(id);
                
                person.ModifiedDateStart = DateTime.UtcNow;
                person.ModifiedDate = null;
                return View(person);
                
            }
            catch (Exception e)
            {
                return RedirectToIndexActionErrorHelper("Unable to retrieve the Record.",e);
                
            }


        }







        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdentificationNo,FName,MName,LName,Sex,SonOfOrWifeOf,NameOfFatherOrHusband,HouseNo,Road,Address2,City,State,Zip,Country,TaxType,GeoLat,GeoLong,BlackListed,Comment,Active,ModifiedDateStart")] Person person)
        //CreatedDateStarted,CreatedDate,CreatedUser,ModifiedDate,ModifiedUser,,Deleted,DeletedByUser,DeleteDate
        {
            if (ModelState.IsValid)
            {

                //db.Entry(person).State = EntityState.Modified;
                try
                {
                    await repo.UpdateAsync(person);
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", MakeErrorMesage(string.Format("ERROR during person Update for '{0}'.",person.FullName), e));
                    return View(person);
                }


                try
                {
                    await repo.SaveAsync();
                    return RedirectToIndexActionHelper(string.Format("The record for '{0}' has been saved.", person.FullName));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", MakeErrorMesage(string.Format("Person Update saving for '{0}'.", person.FullName), e));
                    return View(person);
                }
                //if you are here... there is an error
            }
            return View(person);
        }



        //----------------------------------------------------------------------------------------------------------------------


        // GET: People/Delete/5
        public async Task<ActionResult> Delete(long? id,string message="")
        {
            if (id == null)
            {
                return RedirectToIndexActionHelper("Bad request received. Please try again");
            }
            
            
            
            try
            {
                Person person = await repo.FindForAsync(id);
                if (!string.IsNullOrEmpty(message))
                {
                    message = string.Format("There was an error deleteing {0}. {1}", person.FullName, message);
                    ModelState.AddModelError("", message);
                }
                return View(person);
            }
            catch(Exception e)
            {
                return RedirectToIndexActionErrorHelper("Unable to find the record.", e);
            }


        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {

            try
            {
                await repo.DeleteAsync(id);
                await repo.SaveAsync();
                return RedirectToIndexActionHelper("The record has been deleted. You may undelete it by selecting UNDELETE.");
            }

            catch(Exception ex)
            {
                string message = MakeErrorMesage("",ex ) ;
                return RedirectToAction("Delete", new { id = id, message = message });
            }
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
