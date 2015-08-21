using Bearer.DAL;
using Bearer.Models;
using Bearer.MyPrograms.SetupStrategy.strategies;
using ModelsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.SetupStrategy
{
    public class SetupContext
    {

        private SetUpDAL setupDAL;
        private string user;

        public SetupContext(SetUpDAL setupDALin,string user)
        {
            this.setupDAL = setupDALin;
            this.user = user;

        }

        public ISetupStrategy Create(string setupEnumString)
        {
            SetupEnum setupEnum;
            bool success= Enum.TryParse<SetupEnum>(setupEnumString, out setupEnum);

            try
            {

                if (!success)
                    throw new Exception("The setup Enum did not match. Bad request. Try again");

                switch (setupEnum)
                {
                    case SetupEnum.BccEmailAddress: return new BccMailStrategy(setupDAL, user);
                    case SetupEnum.CompanyName: return new CompanyNameSetupStrategy(setupDAL, user);

                    case SetupEnum.DefaultPageSize: return new DefaultPageSizeStrategy(setupDAL, user);
                    case SetupEnum.EmailTestingDirectory: return new EmailTestingDirectoryStrategy(setupDAL, user);
                    case SetupEnum.FromEmailAddress: return new FromEmailAddressStrategy(setupDAL, user);
                    case SetupEnum.IsSendBcc: return new IsSendBccStrategy(setupDAL, user);
                    case SetupEnum.SendGridOrSmtp: return new SendGridOrSmtpStrategy(setupDAL, user);
                    case SetupEnum.SendGridPassword: return new SendGridPasswordStrategy(setupDAL, user);
                    case SetupEnum.SendGridUserName: return new SendGridUserNameStrategy(setupDAL, user);
                    case SetupEnum.ShowStartUpScreenOnStartup: return new ShowStartUpScreenOnStartupStrategy(setupDAL, user);
                    case SetupEnum.SmsTestingDirectory: return new SmsTestingDirectoryStrategy(setupDAL, user);
                    case SetupEnum.SmtpDomain: return new SmtpDomainStrategy(setupDAL, user);
                    case SetupEnum.SmtpPassword: return new SmtpPasswordStrategy(setupDAL, user);
                    case SetupEnum.SmtpPort: return new SmtpPortStrategy(setupDAL, user);
                    case SetupEnum.SmtpServer: return new SmtpServerStrategy(setupDAL, user);
                    case SetupEnum.SmtpUser: return new SmtpUserStrategy(setupDAL, user);
                    case SetupEnum.WebsiteUrl: return new WebsiteUrlStrategy(setupDAL, user);
                    case SetupEnum.Unknown: return new UnknownStrategy(setupDAL, user);
                    default: return new UnknownStrategy(setupDAL, user); ;
                }
            }
            catch
            { throw; }

        }

    }
}