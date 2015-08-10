using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.Models
{
    public class GlobalValuesVM
    {
        public GlobalValuesVM()
        {

        }

        public GlobalValuesVM(ApplicationDbContext db)
        {
            var s = db.SetUps;

            try
            {
                CompanyName = s.FirstOrDefault(x => x.Name == "CompanyName").Value;

            }
            catch
            {
                CompanyName = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                SendGridUserName = s.FirstOrDefault(x => x.Name == "SendGridUserName").Value;

            }
            catch
            {
                SendGridUserName = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                SendGridPassword = s.FirstOrDefault(x => x.Name == "SendGridPassword").Value;

            }
            catch
            {
                SendGridPassword = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                ShowStartUpScreenOnStartup = s.FirstOrDefault(x => x.Name == "ShowStartUpScreenOnStartup").Value;

            }
            catch
            {
                ShowStartUpScreenOnStartup = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                DefaultPageSize = s.FirstOrDefault(x => x.Name == "DefaultPageSize").Value;

            }
            catch
            {
                DefaultPageSize = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                SmtpServer = s.FirstOrDefault(x => x.Name == "SmtpServer").Value;

            }
            catch
            {
                SmtpServer = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                SmtpUser = s.FirstOrDefault(x => x.Name == "SmtpUser").Value;

            }
            catch
            {
                SmtpUser = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                SmtpPassword = s.FirstOrDefault(x => x.Name == "SmtpPassword").Value;

            }
            catch
            {
                SmtpPassword = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                BccEmailAddress = s.FirstOrDefault(x => x.Name == "BccEmailAddress").Value;
            }
            catch
            {
                BccEmailAddress = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                FromEmailAddress = s.FirstOrDefault(x => x.Name == "FromEmailAddress").Value; 
            }
            catch
            {
                FromEmailAddress = string.Empty;
            }

            //---------------------------------------------------------------------------------
            try
            {
                UseSendgridOrSmtp = s.FirstOrDefault(x => x.Name == "UseSendgridOrSmtp").Value;
            }
            catch
            {
                UseSendgridOrSmtp = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                WebsiteUrl = s.FirstOrDefault(x => x.Name == "WebsiteUrl").Value;
            }
            catch
            {
                WebsiteUrl = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                SmtpPort = s.FirstOrDefault(x => x.Name == "SmtpPort").Value;

            }
            catch
            {
                SmtpPort = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                IsSendBcc = s.FirstOrDefault(x => x.Name == "IsSendBcc").Value;

            }
            catch
            {
                IsSendBcc = string.Empty;

            }
            //---------------------------------------------------------------------------------
            try
            {
                SmtpDomain = s.FirstOrDefault(x => x.Name == "SmtpDomain").Value;

            }
            catch
            {
                SmtpDomain = string.Empty;

            }

        }
        public string CompanyName { get; set; }
        public string SendGridUserName { get; set; }
        public string SendGridPassword { get; set; }
        public string ShowStartUpScreenOnStartup { get; set; }
        public string DefaultPageSize { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string BccEmailAddress { get; set; }
        public string FromEmailAddress { get; set; }
        public string UseSendgridOrSmtp { get; set; }
        public string WebsiteUrl { get; set; }
        public string SmtpPort { get; set; }
        public string IsSendBcc { get; set; }
        public string SmtpDomain { get; set; }

        public string WebsiteAnchorLink { 
            get 
            { 
                return "<a href=\"" + this.WebsiteUrl + "\">"+this.CompanyName+" Website</a>"; 
            }
        }
    }
}