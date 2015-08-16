using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Bearer.Controllers
{
    public class BaseController:Controller
    {
        protected  RedirectToRouteResult RedirectToIndexActionErrorHelper(string message, Exception e)
        {

            message += " Error: " + e.Message;
            if (e.InnerException != null)
                message += " *UGLY SYSTEM MSG*: " + e.InnerException.Message;


            return RedirectToIndexActionHelper(message);

        }
        protected  RedirectToRouteResult RedirectToIndexActionHelper(string message)
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

        protected string MakeErrorMesage(string message, Exception e)
        {
            StringBuilder sb= new StringBuilder();
            
            if(!string.IsNullOrEmpty(message))
            {
                sb.Append(message);
                sb.Append(" ");
            }

            sb.Append(e.Message);

            if(e.InnerException!=null)
            {
                sb.Append(" ");
                sb.Append(e.InnerException.Message);
            }

            return sb.ToString();
                
        }
    }
}