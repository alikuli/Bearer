using Bearer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliKuli
{
    public static class GlobalSetupValues
    {

        /// <summary>
        /// Saves the CompanyName globaly
        /// </summary>
        public static string CompanyName
        {
            get
            {
                return HttpContext.Current.Application["CompanyName"].ToString();
            }

            set
            {
                HttpContext.Current.Application["CompanyName"] = value;
            }
        }

        //---------------------------------------------------------------------------

        ///// <summary>
        ///// This is the User name for email provider sendGrid.com
        ///// </summary>
        //public static string SendGridUserName
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["SendGridUserName"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["SendGridUserName"] = value;
        //    }
        //}


        ////---------------------------------------------------------------------------

        ///// <summary>
        ///// This is the password for the sendGrid.com account
        ///// </summary>
        //public static string SendGridPassword
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["SendGridPassword"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["SendGridPassword"] = value;
        //    }
        //}

        ////---------------------------------------------------------------------------

        ///// <summary>
        ///// If yes, then startup screen shows, otherwise not.
        ///// </summary>
        //public static string ShowStartUpScreenOnStartup
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["ShowStartUpScreenOnStartup"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["ShowStartUpScreenOnStartup"] = value;
        //    }
        //}


        ////---------------------------------------------------------------------------

        ///// <summary>
        ///// This is the default page size used
        ///// </summary>
        //public static string DefaultPageSize
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["DefaultPageSize"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["DefaultPageSize"] = value;
        //    }
        //}

        ////---------------------------------------------------------------------------

        ///// <summary>
        ///// This is the smtp server
        ///// </summary>
        //public static string SmtpServer
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["SmtpServer"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["SmtpServer"] = value;
        //    }
        //}

        ////---------------------------------------------------------------------------

        ////this is the smtp user
        //public static string SmtpUser
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["SmtpUser"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["SmtpUser"] = value;
        //    }
        //}

        ////---------------------------------------------------------------------------

        ///// <summary>
        ///// this is the smtp password
        ///// </summary>
        //public static string SmtpPassword
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["SmtpPassword"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["SmtpPassword"] = value;
        //    }
        //}

        ////---------------------------------------------------------------------------

        ///// <summary>
        ///// This is the BccEmail Address
        ///// </summary>

        //public static string BccEmailAddress
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["BccEmailAddress"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["BccEmailAddress"] = value;
        //    }
        //}

        ////---------------------------------------------------------------------------

        ///// <summary>
        ///// This is the from email address
        ///// </summary>
        //public static string FromEmailAddress
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["FromEmailAddress"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["FromEmailAddress"] = value;
        //    }
        //}

        ////---------------------------------------------------------------------------

        ///// <summary>
        ///// this decides if sendgrid or smtp is used
        ///// </summary>
        //public static string UseSendgridOrSmtp
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["UseSendgridOrSmtp"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["UseSendgridOrSmtp"] = value;
        //    }
        //}

        ///// <summary>
        ///// This is the website url
        ///// </summary>
        //public static string WebsiteUrl
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["WebsiteUrl"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["WebsiteUrl"] = value;
        //    }
        //}

        //public static string SmtpPort
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["SmtpPort"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["SmtpPort"] = value;
        //    }
        //}


        //public static string IsSendBcc
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["IsSendBcc"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["IsSendBcc"] = value;
        //    }
        //}


        //public static string SmtpDomain
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["SmtpDomain"].ToString();
        //    }
        //    set
        //    {
        //        HttpContext.Current.Application["SmtpDomain"] = value;
        //    }
        //}


        ///// <summary>
        ///// This creates a website anchor link
        ///// </summary>
        //public static string WebsiteAnchorLink
        //{
        //    get
        //    {

        //        return "<a href=\"" + AliKuli.GlobalSetupValues.WebsiteUrl + "\">" + AliKuli.GlobalSetupValues.CompanyName + " Website</a>";
        //    }
        //}

        //public static GlobalValuesVM AddAllValues()
        //{
        //    GlobalValuesVM gVm = new GlobalValuesVM();
        //    gVm.BccEmailAddress = BccEmailAddress;
        //    gVm.CompanyName = CompanyName;
        //    gVm.SendGridUserName = SendGridUserName;
        //    gVm.SendGridPassword = SendGridPassword;
        //    gVm.ShowStartUpScreenOnStartup = ShowStartUpScreenOnStartup;
        //    gVm.DefaultPageSize = DefaultPageSize;
        //    gVm.SmtpServer = SmtpServer;
        //    gVm.SmtpUser = SmtpUser;
        //    gVm.SmtpPassword = SmtpPassword;
        //    gVm.BccEmailAddress = BccEmailAddress;
        //    gVm.FromEmailAddress = FromEmailAddress;
        //    gVm.UseSendgridOrSmtp = UseSendgridOrSmtp;
        //    gVm.WebsiteUrl = WebsiteUrl;
        //    gVm.SmtpPort = SmtpPort;
        //    gVm.IsSendBcc = IsSendBcc;
        //    gVm.SmtpDomain = SmtpDomain;
        //    return gVm;

        //}
    }



}