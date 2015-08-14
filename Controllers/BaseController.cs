using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bearer.Controllers
{
    public class BaseController:Controller
    {
        public  RedirectToRouteResult RedirectToIndexActionErrorHelper(string message, Exception e)
        {

            message += " Try Again. Error: " + e.Message;
            if (e.InnerException != null)
                message += " SYSTEM MSG: " + e.InnerException.Message;


            return RedirectToIndexActionHelper(message);

        }
        public  RedirectToRouteResult RedirectToIndexActionHelper(string message)
        {

            return RedirectToAction("Index", new { message = message });

        }



        /// <summary>
        /// Access denied view
        /// </summary>
        /// <returns>Access denied view</returns>
        protected ActionResult AccessDeniedView()
        {
            //return new HttpUnauthorizedResult();
            return RedirectToAction("AccessDenied", "Security", new { pageUrl = this.Request.RawUrl });
        }


    }
}