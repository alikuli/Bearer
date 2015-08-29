using AliKuli.Exceptions;
using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.DAL;
using ModelsClassLibrary.Interfaces;
using ModelsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bearer.Controllers
{
    public abstract class AbstractController<T>:BaseController,IController<T> where T :ICommon, new()
    {
        //private ApplicationDbContext db;
        protected IRepositry<T> repo;
        //private string userName = string.Empty;


        public AbstractController()
        {

        }
        public AbstractController(IRepositry<T> _repo)
        {
            repo = _repo;
        }

        
        
        // GET: entitys
        public async virtual Task<ActionResult> Index(string message = "")
        {

            //Type t = typeof(T);
            //bool hasNameProperty= t.GetProperty("Name") != null;
            



            //return View(await db.entitys.ToListAsync());
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("", message);
            }

            try
            {
                return View(await repo.FindAllAsync());

            }
            catch (Exception e)
            {
                MakeErrorMesage("Something went wrong. Try again.", e);


                return View();
            }

        }


        //----------------------------------------------------------------





        // GET: entitys/Details/5
        public  async virtual Task<ActionResult> Details(long? id)
        {

            try
            {
                var entity = await repo.FindForAsync(id);
                return View(entity);
            }
            catch (Exception e)
            {
                return RedirectToIndexActionErrorHelper("There was an error.", e);
            }

        }





        //----------------------------------------------------------------


        // GET: entitys/Create
        public  virtual ActionResult Create()
        {
            //return View();
            T l = (T)Activator.CreateInstance(typeof(T));

            //this is when we start to create the record
            l.CreatedDateStarted = new DateTimeAdapter().UtcNow;
            return View(l);

        }



        //----------------------------------------------------------------

        

        
        // POST: entitys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public virtual async Task<ActionResult> Create([Bind(Include = "Name,Comment,CreatedDateStarted,PhoneIntlCode,Abbreviation,HouseNo,Road,Address2,City,State,Zip,CountryID")] T entity)
        //Id,CreatedDate,CreatedUser,ModifiedDateStart,ModifiedDate,ModifiedUser,Active,Deleted,DeletedByUser,DeleteDate,UnDeletedByUser,UnDeleteDate
        {
            //if (ModelState.IsValid)
            //{
            //    db.entitys.Add(entity);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}

            //return View(entity);

            if (ModelState.IsValid)
            {

                try
                {
                    repo.Create(entity);
                    await repo.SaveAsync();
                    return RedirectToIndexActionHelper(string.Format("The entity has been saved" ));

                }
                catch (NoDuplicateException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (Exception e)
                {
                    string message = MakeErrorMesage("Unable to create.", e);
                    ModelState.AddModelError("", message);
                }
            }
            //ModelState.AddModelError("", string.Format("There was an error. The entity did not get saved." ));
            return View(entity);
        
        
        }


        
        
        //----------------------------------------------------------------

        
        
        
        
        // GET: entitys/Edit/5
        public virtual async Task<ActionResult> Edit(long? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //entity entity = await db.entitys.FindAsync(id);
            //if (entity == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(entity);

            try
            {
                T entity = await repo.FindForAsync(id);

                entity.ModifiedDateStart = new DateTimeAdapter().UtcNow;
                entity.ModifiedDate = null;
                return View(entity);

            }
            catch (Exception e)
            {
                return RedirectToIndexActionErrorHelper("Unable to retrieve the Record.", e);

            }

        }





        //----------------------------------------------------------------






        // POST: entitys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async virtual Task<ActionResult> Edit([Bind(Include = "Id,Name,Comment,ModifiedDateStart,PhoneIntlCode,Abbreviation,HouseNo,Road,Address2,City,State,Zip,CountryID")] T entity)

            //CreatedDateStarted,CreatedDate,CreatedUser,ModifiedDate,ModifiedUser,,Active,Deleted,DeletedByUser,DeleteDate,UnDeletedByUser,UnDeleteDate
        {

            if (ModelState.IsValid)
            {

                //db.Entry(entity).State = EntityState.Modified;
                try
                {
                    await repo.UpdateAsync(entity);

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", MakeErrorMesage(string.Format("ERROR during entity Update."), e));
                    return View(entity);
                }


                try
                {
                    await repo.SaveAsync();
                    //repo.Save();
                    return RedirectToIndexActionHelper(string.Format("The record has been saved."));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", MakeErrorMesage(string.Format("ERROR during entity Update saving."), e));
                    return View(entity);
                }
            }
            //if you are here... there is an error
            return View(entity);

        }





        //----------------------------------------------------------------






        // GET: entitys/Delete/5
        public async virtual Task<ActionResult> Delete(long? id, string message = "")
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //entity entity = await db.entitys.FindAsync(id);
            //if (entity == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(entity);
            try
            {
                T entity = await repo.FindForAsync(id);

                if (!string.IsNullOrEmpty(message))
                {
                    message = string.Format("Error. Cannot Delete. {0}",  message);
                    ModelState.AddModelError("", message);
                }
                return View(entity);
            }
            catch (Exception e)
            {
                return RedirectToIndexActionErrorHelper("Unable to find the record.", e);
            }


        }



        //----------------------------------------------------------------




        // POST: entitys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async virtual Task<ActionResult> DeleteConfirmed(long id)
        {
            //entity entity = await db.entitys.FindAsync(id);
            //db.entitys.Remove(entity);
            //await db.SaveChangesAsync();
            //return RedirectToAction("Index");

            try
            {
                await repo.DeleteAsync(id);
                await repo.SaveAsync();
                return RedirectToIndexActionHelper("The record has been deleted. You may undelete it by selecting UNDELETE.");
            }

            catch (Exception ex)
            {
                string message = MakeErrorMesage("", ex);
                return RedirectToAction("Delete", new { id = id, message = message });
            }

        }



        //----------------------------------------------------------------


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }




    }

}
