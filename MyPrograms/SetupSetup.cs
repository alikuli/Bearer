<<<<<<< HEAD
﻿using Bearer.DAL;
//using AppDbx.Models;
using Bearer.Models;
using ModelsClassLibrary.Models;
=======
﻿using AppDbx.Models;
//using Bearer.Models;
>>>>>>> Persons-fork
using System;
using System.Linq;
using System.Web;


namespace Bearer.MyPrograms
{
    public class SetupInitialize
    {
        ApplicationDbContext db;
        SetUpDAL repo;

        public SetupInitialize (ApplicationDbContext dbIn, string user)
	    {
            db=dbIn;
            repo = new SetUpDAL(db, user);
    	}


        //public void AddCompanyName()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = AliKuli.MyConstants.SetupMyConstant.CompanyNameField;
        //    s.Description = "*Misc: Company Name";
        //    s.Type = EnumTypes.String;
        //    s.Value = "Enter Company Name";
        //    AddToSetup(s);

        //}

        //public void AddSendGridPassword()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = AliKuli.MyConstants.SetupMyConstant.Field_AddSendGridPassword;
        //    s.Description = "Email SendGrid: sendgrid.com Password ";
        //    s.Type = EnumTypes.String;
        //    s.Value = "Enter Your sendgrid.com Password";
        //    AddToSetup(s);

        //}


        //public void AddFromEmailAddress()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "FromEmailAddress";
        //    s.Description = "*Email: From Email Address";
        //    s.Type = EnumTypes.String;
        //    s.Value = "Enter Your FROM email Address";
        //    AddToSetup(s);

        //}
        //public void AddSendGridUserName()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "SendGridUserName";
        //    s.Description = "Email SendGrid: sendgrid.com User Id";
        //    s.Type = EnumTypes.String;
        //    s.Value = "Enter Your sendgrid.com User Id";
        //    AddToSetup(s);
        //}
        //public void AddShowStartUpScreenOnStartup()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "ShowStartUpScreenOnStartup";
        //    s.Description = "*Misc: Show Startup Screen on Start Up?";
        //    s.Type = EnumTypes.boolean;
        //    s.Value = "true";
        //    AddToSetup(s);
        //}
        ////public void AddDefaultPageSize()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "DefaultPageSize";
        //    s.Description = "*Misc: Default No of Records per screen";
        //    s.Type = EnumTypes.Integer;
        //    s.Value = "10";
        //    AddToSetup(s);
        //}
        //public void AddSmtpServer()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "SmtpServer";
        //    s.Description = "Email SMTP: Enter SMTP Server";
        //    s.Type = EnumTypes.String;
        //    s.Value = "SmtpServer";
        //    AddToSetup(s);
        ////}
        //public void AddSmtpUser()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "SmtpUser";
        //    s.Description = "Email SMTP: Smtp User";
        //    s.Type = EnumTypes.String;
        //    s.Value = "SmtpUser";
        //    AddToSetup(s);
        //}
        //public void AddSmtpPassword()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "SmtpPassword";
        //    s.Description = "Email SMTP: Smtp Password";
        //    s.Type = EnumTypes.String;
        //    s.Value = "SmtpPassword";
        //    AddToSetup(s);
        //}
        //public void AddBccEmailAddress()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "BccEmailAddress";
        //    s.Description = "*Email: BCC Email Address -Address where you receive blind emails eg. Login emails";
        //    s.Type = EnumTypes.String;
        //    s.Value = "BccEmailAddress";
        //    AddToSetup(s);
        //}
        //public void AddWebsiteUrl()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "WebsiteUrl";
        //    s.Description = "*Misc: Your website URL";
        //    s.Type = EnumTypes.String;
        //    s.Value = "Add Website Url";
        //    AddToSetup(s);
        //}
        //public void AddIsSendBcc()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "IsSendBcc";
        //    s.Description = "*Email: Do you want BCC messages?";
        //    s.Type = EnumTypes.boolean;
        //    s.Value = "true";
        //    AddToSetup(s);
        //}
        ////public void AddUseSendgridOrSmtp()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "UseSendgridOrSmtp";
        //    s.Description = "*Email: Use smptp or Sendgrid";
        //    s.Type = EnumTypes.EmailingMethod;
        //    s.Value = "smtp";
        //    AddToSetup(s);
        //}
        //public void AddSmtpPort()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "SmtpPort";
        //    s.Description = "Email SMTP: SMTP Port";
        //    s.Type = EnumTypes.Integer;
        //    s.Value = "21";
        //    AddToSetup(s);
        //}
        //public void AddSmtpDomain()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "SmtpDomain";
        //    s.Description = "Email SMTP: Your SMTP Domain";
        //    s.Type = EnumTypes.String;
        //    s.Value = "";
        //    AddToSetup(s);
        //}
        //public void AddSmsTestDirectory()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "SmsTestingDirectory";
        //    s.Description = "SMS Testing Directory: test SMS Land here";
        //    s.Type = EnumTypes.FilePath;
        //    s.Value = @"c:\TestSms\";
        //    AddToSetup(s);
        //}
        //public void AddEmailTestDirectory()
        //{
        //    SetUp s = new SetUp();
        //    s.Name = "EmailTestingDirectory";
        //    s.Description = "Email Testing Directory: Test emails land here.";
        //    s.Type = EnumTypes.FilePath;
        //    s.Value = @"c:\TestEmails\"; 
        //    AddToSetup(s);
        //}
        public void Initialize(string theField = "")
        {
            //if (string.IsNullOrEmpty(theField))
            //{
            //    //Add the setups
            //    AddCompanyName();
            //    AddSendGridPassword();
            //    AddSendGridUserName();
            //    AddFromEmailAddress();
            //    AddShowStartUpScreenOnStartup();
            //    AddDefaultPageSize();
            //    AddSmtpServer();
            //    AddSmtpUser();
            //    AddSmtpPassword();
            //    AddBccEmailAddress();
            //    AddWebsiteUrl();
            //    AddIsSendBcc();
            //    AddUseSendgridOrSmtp();
            //    AddSmtpPort();
            //    AddSmtpDomain();
            //    AddSmsTestDirectory();
            //    AddEmailTestDirectory();
            //}
            //else
            //{
            //    switch (theField.ToLower())
            //        {

            //            case "companyname":
            //                AddCompanyName();
            //                break;

            //            case "sendgridpassword":
            //                AddSendGridPassword();
            //                break;

            //            case "sendgriduserName":
            //                AddSendGridUserName();
            //                break;

            //            case "fromemailaddress":
            //                AddFromEmailAddress();
            //                break;
                        
            //            case "showstartUpscreenonstartup":
            //                AddShowStartUpScreenOnStartup();

            //                break;
            //            case "defaultpagesize":
            //                AddDefaultPageSize();

            //                break;
            //            case "smtpserver":
            //                AddSmtpServer();

            //                break;
            //            case "smtpuser":
            //                AddSmtpUser();

            //                break;
            //            case "smtppassword":
            //                AddSmtpPassword();

            //                break;
            //            case "bccemailaddress":
            //                AddBccEmailAddress();

            //                break;
            //            case "Websiteurl":
            //                AddWebsiteUrl();

            //                break;
            //            case "issendbcc":
            //                AddIsSendBcc();

            //                break;
            //            case "usesendgridorsmtp":
            //                AddUseSendgridOrSmtp();

            //                break;
            //            case "smtpport":
            //                AddSmtpPort();

            //                break;
            //            case "smtpdomain":
            //                AddSmtpDomain();
            //                break;

            //            case "SmsTestingDirectory":
            //                AddSmsTestDirectory();
            //                break;
            //            case "EmailTestingDirectory":
            //                AddEmailTestDirectory();
            //                break;

            //            default:
            //                throw new Exception ("In Setup - couldnt find a field to add. Please check all setup fields");
            //        }
            //}

          

        }

        public void LoadIntoMemory()
        {
            //LOAD ALL THE VARIABLES INTO APPLICTION STATE
            
            //string coName=
            //        db.SetUps.FirstOrDefault(x => x.Name == "CompanyName").Value != null ?
            //        db.SetUps.FirstOrDefault(x => x.Name == "CompanyName").Value.ToString() :
            //        string.Empty;

            //new GlobalValuesVM().CompanyName = coName;
            //AliKuli.GlobalSetupValues.CompanyName = coName;

            //HttpContext.Current.Application["CompanyName"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "CompanyName").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "CompanyName").Value.ToString() :
            //    string.Empty;

            //HttpContext.Current.Application["SendGridPassword"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "SendGridPassword").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "SendGridPassword").Value.ToString() :
            //    string.Empty;

            //HttpContext.Current.Application["SendGridUserName"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "SendGridUserName").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "SendGridUserName").Value.ToString() :
            //    string.Empty;

            //HttpContext.Current.Application["ShowStartUpScreenOnStartup"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "ShowStartUpScreenOnStartup").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "ShowStartUpScreenOnStartup").Value.ToString() :
            //    string.Empty;

            //HttpContext.Current.Application["DefaultPageSize"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "DefaultPageSize") != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "DefaultPageSize").Value.ToString() :
            //    string.Empty;

            //HttpContext.Current.Application["SmtpServer"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "SmtpServer").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "SmtpServer").Value.ToString() :
            //    string.Empty;

            //HttpContext.Current.Application["SmtpUser"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "SmtpUser").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "SmtpUser").Value.ToString() :
            //    string.Empty;

            ////This is the smptp password
            //HttpContext.Current.Application["SmtpPassword"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "SmtpPassword").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "SmtpPassword").Value.ToString() :
            //    string.Empty;

            ////This controls if BCC are sent
            //HttpContext.Current.Application["IsSendBcc"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "IsSendBcc").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "IsSendBcc").Value.ToString() :
            //    string.Empty;

            ////This is where all the BCC emails go
            //HttpContext.Current.Application["BccEmailAddress"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "BccEmailAddress").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "BccEmailAddress").Value.ToString() :
            //    string.Empty;

            ////This is the from email address used in the program
            //HttpContext.Current.Application["FromEmailAddress"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "FromEmailAddress").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "FromEmailAddress").Value.ToString() :
            //    string.Empty;

            ////This is the website URL for the current website
            //HttpContext.Current.Application["WebsiteUrl"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "WebsiteUrl").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "WebsiteUrl").Value.ToString() :
            //    string.Empty;

            ////This decides if sendgrid is used or a smtp
            //HttpContext.Current.Application["UseSendgridOrSmtp"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "UseSendgridOrSmtp").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "UseSendgridOrSmtp").Value.ToString() :
            //    string.Empty;

            ////This decides if sendgrid is used or a smtp
            //HttpContext.Current.Application["SmtpPort"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "SmtpPort").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "SmtpPort").Value.ToString() :
            //    string.Empty;

            ////This is the smtp domain
            //HttpContext.Current.Application["SmtpDomain"] =
            //    db.SetUps.FirstOrDefault(x => x.Name == "SmtpDomain").Value != null ?
            //    db.SetUps.FirstOrDefault(x => x.Name == "SmtpDomain").Value.ToString() :
            //    string.Empty;

        }
        protected void AddToSetup(SetUp s)
        {

            try
            {
                repo.Create(s);
                repo.Save();
            }
            catch 
            {
                
            }
        }

        
    }
}