using Bearer.Models;
using Microsoft.AspNet.Identity;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Bearer.MyPrograms.EmailStrategy
{
    public class SendGridEmailStrategy:EmailStrategyAbstract
    {
        public override async Task SendAsync(IdentityMessage message, GlobalValuesVM g)
        {
            string sendGridUserName = g.SendGridUserName;
            string sendGridPassword = g.SendGridPassword;
            string fromEmailAddress = g.FromEmailAddress;
            string bccEmailAddress = g.BccEmailAddress;
            string subject = message.Subject;
            string body = message.Body;

            var myMessage = new SendGridMessage();

            myMessage.AddTo(message.Destination);
            myMessage.From = new System.Net.Mail.MailAddress(fromEmailAddress);
            myMessage.AddBcc(bccEmailAddress);

            myMessage.Subject = subject;
            myMessage.Text = body;
            myMessage.Html = body;

            var credentials = new NetworkCredential(
                sendGridUserName,
                sendGridPassword);

            //Create the transport
            var transportWeb = new Web(credentials);

            //Send Email
            if (transportWeb != null)
            {
                try
                {
                    //sendGridUserName
                    if (sendGridUserName.ToLower() == string.Format("Enter Your sendgrid.com User Id").ToLower())
                        throw new Exception("You have not entered your Send Grid USER NAME");

                    //sendGridPassword
                    if (sendGridPassword.ToLower() == string.Format("Enter Your sendgrid.com Password").ToLower())
                        throw new Exception("You have not entered your Send Grid PASSWORD");


                    await transportWeb.DeliverAsync(myMessage);
                }
                catch (Exception ex)
                {
                    throw new HttpException("EMAIL Exception. Unable to resolve the remote server. Please try again later. Error: " + ex.Message);
                }
            }
            else
            {
                System.Diagnostics.Trace.TraceError("Failed to create a Web Transport");
                await Task.FromResult(0);
                throw new HttpException("EMAIL Exception. Failed to create a Web Transport");
            }

        }
    }
}