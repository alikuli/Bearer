using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

using System.Net;
using SendGrid;
using System.Net.Mail;
using System.IO;
using System.Text;
//using AppDbx.Models;
using Bearer.Models;
using Bearer.MyPrograms.EmailStrategy;
using ModelsClassLibrary.Models.SMS;
using Bearer.MyPrograms.SmsStrategy;


namespace Bearer
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {

            // Plug in your email service here to send an email.
            ApplicationDbContext db = new ApplicationDbContext();

            //var setUpVariable = db.SetUps;
            GlobalValuesVM g = new GlobalValuesVM(db);


            try
            {


                //this will select the email strategy


                string fromEmailAddress = g.FromEmailAddress;
                string bccEmailAddress = g.BccEmailAddress;
                string isSendBcc = g.IsSendBcc;


                if (!AliKuli.Validators.MyValidators.IsValidEmail(fromEmailAddress))
                    throw new Exception("Invalid FROM email address.");

                if (!AliKuli.Validators.MyValidators.IsValidEmail(message.Destination))
                    throw new Exception("Invalid TO email address.");

                if (Boolean.Parse(isSendBcc))
                    if (!AliKuli.Validators.MyValidators.IsValidEmail(bccEmailAddress))
                        throw new Exception("Invalid bcc email address. Fix the address or switch off BCC from Setup");



            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                //string emailType = g.UseSendgridOrSmtp;
                //IEmailStrategy emailStrategy =  EmailContext.CreateProvider(emailType);
                IEmailStrategy emailStrategy = new TestEmailStrategy();

                await emailStrategy.SendAsync(message, g);

            }
            catch
            { throw; }
        }
    
    //    private static void TestEmail(IdentityMessage message, GlobalValuesVM g)
    //    {
    //        string currDir = @"c:\TestEmails\";
    //        string nameOfEmail = Path.Combine(currDir, "email_" +message.Destination + "_"+DateTime.Now.Ticks.ToString() + ".txt");

    //        string[] strArray= new string[4];
    //        strArray[0] = "TO: " + message.Destination ;
    //        strArray[1]= "FROM: "+ g.FromEmailAddress;
    //        strArray[2]= "SUBJECT:" + message.Subject;
    //        strArray[3] = message.Body;

    //        File.WriteAllLines(nameOfEmail,strArray);



    //    }
    //    private async Task configSendGridasync(IdentityMessage message, GlobalValuesVM g)
    //    {
            
    //        string sendGridUserName = g.SendGridUserName;
    //        string sendGridPassword = g.SendGridPassword;
    //        string fromEmailAddress = g.FromEmailAddress;
    //        string bccEmailAddress = g.BccEmailAddress;

    //        var myMessage = new SendGridMessage();
    //        myMessage.AddTo(message.Destination);
    //        myMessage.From = new System.Net.Mail.MailAddress(fromEmailAddress);
    //        myMessage.AddBcc(bccEmailAddress );

    //        myMessage.Subject = message.Subject;
    //        myMessage.Text = message.Body;
    //        myMessage.Html = message.Body;

    //        var credentials = new NetworkCredential(
    //            sendGridUserName,
    //            sendGridPassword);

    //        //Create the transport
    //        var transportWeb = new Web(credentials);

    //        //Send Email
    //        if (transportWeb != null)
    //        {
    //            try
    //            {
    //                //sendGridUserName
    //                if (sendGridUserName.ToLower() == string.Format("Enter Your sendgrid.com User Id").ToLower())
    //                    throw new Exception("You have not entered your Send Grid USER NAME");

    //                //sendGridPassword
    //                if (sendGridPassword.ToLower() == string.Format("Enter Your sendgrid.com Password").ToLower())
    //                    throw new Exception("You have not entered your Send Grid PASSWORD");


    //                await transportWeb.DeliverAsync(myMessage);
    //            }
    //            catch (Exception ex)
    //            {
    //                throw new HttpException("EMAIL Exception. Unable to resolve the remote server. Please try again later. Error: " + ex.Message);
    //            }
    //        }
    //        else
    //        {
    //            System.Diagnostics.Trace.TraceError("Failed to create a Web Transport");
    //            await Task.FromResult(0);
    //            throw new HttpException("EMAIL Exception. Failed to create a Web Transport");
    //        }
    //    }

    //    private async Task ConfigSendSmtp(IdentityMessage message, GlobalValuesVM g)
    //    {

    //        //getting values from db
    //        string messageFrom = !string.IsNullOrEmpty(g.FromEmailAddress)?g.FromEmailAddress.ToLower(): string.Empty;
    //        string host = !string.IsNullOrEmpty(g.SmtpServer) ? g.SmtpServer.ToLower() : string.Empty;
    //        int port = !string.IsNullOrEmpty(g.SmtpServer) ? int.Parse(g.SmtpPort) : 0;
    //        string smtpPassword = !string.IsNullOrEmpty(g.SmtpPassword) ? g.SmtpPassword : string.Empty;
    //        string smtpUser = !string.IsNullOrEmpty(g.SmtpUser) ? g.SmtpUser.ToLower() : string.Empty;
    //        string smtpDomain = !string.IsNullOrEmpty(g.SmtpDomain) ? g.SmtpDomain.ToLower() : string.Empty;
    //        string messageBccEmailAddress = !string.IsNullOrEmpty(g.BccEmailAddress) ? g.BccEmailAddress.ToLower() : "false";

            
            
    //        using (SmtpClient smtpClient = new SmtpClient())
    //        {

    //            smtpClient.UseDefaultCredentials = false;

    //            if (string.IsNullOrEmpty(smtpDomain))
    //                smtpClient.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);
    //            else
    //                smtpClient.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword, smtpDomain);

    //            //smtpClient.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);
    //            smtpClient.EnableSsl = true;
    //            smtpClient.Host = host;
    //            smtpClient.Port = port;
    //            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
    //            smtpClient.Timeout = 300000;



    //            //send
    //            try
    //            {
    //                if (!AliKuli.Validators.MyValidators.IsValidEmail(messageFrom))
    //                    throw new Exception("FROM email is not valid.");

    //                if (!AliKuli.Validators.MyValidators.IsValidEmail(messageBccEmailAddress))
    //                    throw new Exception("BCC email is not valid.");

    //                if (!AliKuli.Validators.MyValidators.IsValidEmail(message.Destination))
    //                    throw new Exception("TO email is not valid.");

    //                //error catching
    //                //IsSendBcc
    //                if (Boolean.Parse( g.IsSendBcc))
    //                    messageBccEmailAddress =  g.BccEmailAddress;

    //                //SmtpPassword
    //                if (smtpPassword.ToLower()==(string.Format("SmtpPassword").ToLower()))
    //                    throw new Exception("SMTP Password has not been set.");

    //                //SmtpUser
    //                if (smtpUser.ToLower()==(string.Format("SmtpUser").ToLower()))
    //                    throw new Exception("SMTP User has not been set.");

    //                //SMTP Domain
    //                if (smtpDomain.ToLower() == string.Format("Your SMTP Domain").ToLower())
    //                    throw new Exception("SMTP Domain has not been set.");



    //                //MailMessage m = new MailMessage(messageFrom, message.Destination, message.Subject, message.Body);
    //                MailMessage m = new MailMessage();
    //                m.From = new MailAddress(messageFrom);
    //                m.To.Add(message.Destination);
    //                m.Subject = message.Subject;
    //                m.Body = message.Body;
    //                m.IsBodyHtml = true;
                    
    //                 await smtpClient.SendMailAsync(m);
    //            }
    //            catch (Exception)
    //            {
    //                throw;
    //            }
    //        }   
    //    }

 


    }

    public class SmsService : IIdentityMessageService
    {
        public  Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            //return Task.FromResult(0);

            ISmsStrategy smsStrategy =  SmsContext.CreateSmsStrategy(SmsStrategyEnum.TESTER);

            try
            {
                ApplicationDbContext db = new ApplicationDbContext();

                //var setUpVariable = db.SetUps;
                GlobalValuesVM g = new GlobalValuesVM(db);
                smsStrategy.SendAsync(message, g);

                return Task.FromResult(0);
            }
            catch
            { throw; }


        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static UserManager Create(IdentityFactoryOptions<UserManager> options, IOwinContext context) 
        {
            
            var manager = new UserManager(new UserStore<User>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false,

            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            
            
            
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();





            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<User, string>
    {
        public ApplicationSignInManager(UserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<UserManager>(), context.Authentication);
        }
    }
}
