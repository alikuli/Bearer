using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using System.Text;
//using AppDbx.Models;
using Bearer.Models;
using ModelsClassLibrary.Models;

namespace Bearer.Controllers
{
    
    [Authorize(Roles=AliKuli.MyConstants.AdminConst)]
    public class UsersController : Controller
    {
        private ApplicationDbContext db;
        private GlobalValuesVM globalValues;

        public UsersController()
        {
            db = new ApplicationDbContext();
            globalValues = new GlobalValuesVM(db);

        }   
        
        
        
        
        // GET: Users
        public async Task<ActionResult> Index(string userError, string errorMessage)
        {
            if (!string.IsNullOrEmpty(userError))
            {
                if (userError == "ActivationEmailSent")
                {
                    ModelState.AddModelError("", errorMessage);
                }
                if (userError == "ActivationEmailSentError")
                {
                    ModelState.AddModelError("", errorMessage);
                }
            }

            return View(await db.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return RedirectToAction("Register","Account");
        }

        //// POST: Users/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Active,CreatedDate,CreatedUser,ModifiedDate,ModifiedUser,Comment,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(user);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(user);
        //}

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Active,Comment,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                var userDb = db.Users.Find(user.Id);

                if (userDb == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                bool userActivated = !userDb.Active && user.Active;

                userDb.Active = user.Active;
                userDb.Comment = user.Comment;
                userDb.Email = user.Email;
                userDb.EmailConfirmed = user.EmailConfirmed;
                userDb.PhoneNumber = user.PhoneNumber;
                userDb.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                //userDb.UserName = user.UserName;

                userDb.ModifiedDate = new DateTimeAdapter().UtcNow;
                userDb.ModifiedUser = User.Identity.Name;

                db.Entry(userDb).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Could not update the user. Please try again. Error: " + e.Message);
                    return View(userDb);
                }
                if(userActivated)
                {
                    
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<p>");
                    sb.Append("Dear: " + userDb.UserName);
                    sb.Append("</p>");

                    sb.Append("<p>");
                    sb.Append("Please note, your account has been activated. Please log into " + globalValues.WebsiteAnchorLink);
                    sb.Append("</p>");
                    
                    sb.Append("<p>");
                    sb.Append("Best Regards");
                    sb.Append("</p>");

                    sb.Append("<p>");
                    sb.Append("Management " + globalValues.CompanyName);
                    sb.Append("</p>");

                    IdentityMessage message = new IdentityMessage();
                    message.Body = sb.ToString();
                    message.Destination = userDb.Email;
                    message.Subject = globalValues.CompanyName + " Website: Account has been activated!";
                    
                    EmailService es = new EmailService();
                    try
                    {
                        await es.SendAsync(message);
                    }
                    catch (Exception e)
                    {
                        string errorMessage = "Error! Email NOT sent - " + e.Message;
                        return RedirectToAction("Index", new { userError = "ActivationEmailSentError", errorMessage=errorMessage });
                    }
                    string e_message = string.Format("Activation Email sent to Email: '{0}' -User: {1}  ", user.Email, userDb.UserName);
                    return RedirectToAction("Index", new { userError = "ActivationEmailSent", errorMessage = e_message });
                }
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult SaveLastLogin(string id, string returnUrl)
        {
            return  SaveLastLoginHttp(id,returnUrl) ;

        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveLastLoginHttp(string id, string returnUrl)
        {
                var userDb = db.Users.Find(id);

                if (userDb == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                userDb.LastLogin = new DateTimeAdapter().UtcNow;
                userDb.NoOfLogins++;
                userDb.IpAddressOfLastLogin = Request.UserHostAddress;

                db.Entry(userDb).State = EntityState.Modified;
                db.SaveChanges();

                //return RedirectToLocal(returnUrl);
                return RedirectToAction("Index", "Startup");
        }

        [AllowAnonymous]
        public ActionResult SaveLastLockout(string id)
        {
            return SaveLastLockoutHttp(id);

        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveLastLockoutHttp(string id)
        {
            var userDb = db.Users.Find(id);

            if (userDb == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDb.LastLockout = new DateTimeAdapter().UtcNow;
            db.Entry(userDb).State = EntityState.Modified;
            db.SaveChangesAsync();

            return View("Lockout");

        }

        [AllowAnonymous]
        public ActionResult SaveLastLoginFailure(string id, string returnUrl)
        {
            return SaveLastLoginFailureHttp(id, returnUrl);

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveLastLoginFailureHttp(string id, string returnUrl)
        {
            var userDb = db.Users.Find(id);

            if (userDb == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDb.LastSignInFailure = new DateTimeAdapter().UtcNow;
            db.Entry(userDb).State = EntityState.Modified;
            db.SaveChanges();
            string showInvalidLoginError = "true";
            return RedirectToAction("Login", "Account", new { showInvalidLoginError = showInvalidLoginError, returnUrl = returnUrl });

        }

        
        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            User user =  db.Users.Find(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    private ActionResult RedirectToLocal(string returnUrl)
        {
            //if (Url.IsLocalUrl(returnUrl))
            //{
            //    return Redirect(returnUrl);
            //}
            return RedirectToAction("Index", "Home");
        }
    }
}
