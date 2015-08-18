using Bearer.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Bearer.MyPrograms.EmailStrategy
{
    public class SmtpEmailStrategy:EmailStrategyAbstract
    {
        public async override Task SendAsync(IdentityMessage message, GlobalValuesVM g)
        {
            //getting values from db
            string messageFrom = !string.IsNullOrEmpty(g.FromEmailAddress) ? g.FromEmailAddress.ToLower() : string.Empty;
            string host = !string.IsNullOrEmpty(g.SmtpServer) ? g.SmtpServer.ToLower() : string.Empty;
            int port = !string.IsNullOrEmpty(g.SmtpServer) ? int.Parse(g.SmtpPort) : 0;
            string smtpPassword = !string.IsNullOrEmpty(g.SmtpPassword) ? g.SmtpPassword : string.Empty;
            string smtpUser = !string.IsNullOrEmpty(g.SmtpUser) ? g.SmtpUser.ToLower() : string.Empty;
            string smtpDomain = !string.IsNullOrEmpty(g.SmtpDomain) ? g.SmtpDomain.ToLower() : string.Empty;
            string messageBccEmailAddress = !string.IsNullOrEmpty(g.BccEmailAddress) ? g.BccEmailAddress.ToLower() : "false";
            string subject = message.Subject;
            string body = message.Body;
            string destination = message.Destination;

            using (SmtpClient smtpClient = new SmtpClient())
            {

                smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);
                smtpClient.EnableSsl = true;
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Timeout = 300000;



                //send
                try
                {
                    bool IsSendBcc = bool.Parse(g.IsSendBcc);

                    if (string.IsNullOrEmpty(smtpDomain))
                        smtpClient.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);
                    else
                        smtpClient.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword, smtpDomain);

                    if (!AliKuli.Validators.MyValidators.IsValidEmail(messageFrom))
                        throw new Exception("FROM email is not valid.");

                    if (!AliKuli.Validators.MyValidators.IsValidEmail(messageBccEmailAddress))
                        throw new Exception("BCC email is not valid.");

                    if (!AliKuli.Validators.MyValidators.IsValidEmail(destination))
                        throw new Exception("TO email is not valid.");

                    //error catching
                    //IsSendBcc
                    if (IsSendBcc)
                        messageBccEmailAddress = g.BccEmailAddress;

                    //SmtpPassword
                    if (smtpPassword.ToLower() == (string.Format("SmtpPassword").ToLower()))
                        throw new Exception("SMTP Password has not been set.");

                    //SmtpUser
                    if (smtpUser.ToLower() == (string.Format("SmtpUser").ToLower()))
                        throw new Exception("SMTP User has not been set.");

                    //SMTP Domain
                    if (smtpDomain.ToLower() == string.Format("Your SMTP Domain").ToLower())
                        throw new Exception("SMTP Domain has not been set.");



                    //MailMessage m = new MailMessage(messageFrom, message.Destination, message.Subject, message.Body);
                    MailMessage m = new MailMessage();
                    m.From = new MailAddress(messageFrom);
                    m.To.Add(destination);
                    m.Subject = subject;
                    m.Body = body;
                    m.IsBodyHtml = true;

                    await smtpClient.SendMailAsync(m);
                }
                catch (Exception)
                {
                    throw;
                }
            }   

        }
    }
}