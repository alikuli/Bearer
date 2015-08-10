using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace AliKuli.Validators
{
    public static class MyValidators
    {
        /// <summary>
        /// This validates email addresses
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                if (string.IsNullOrEmpty(emailaddress))
                    return false;
                //    throw new Exception("The Email address is empty!");
                
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }


        public static bool IsValidUrl(string uriName)
        {
            Uri uriResult;
            return Uri.TryCreate(uriName, UriKind.Absolute, out uriResult)
                     && (uriResult.Scheme == Uri.UriSchemeHttp
                     || uriResult.Scheme == Uri.UriSchemeHttps);

        }
    }
}