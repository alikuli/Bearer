using Bearer.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.EmailStrategy
{
    public static class EmailContext
    {
        public static IEmailStrategy CreateProvider(string selectedProvider)
        {
            switch (selectedProvider.ToLower())
            {
                case "smtp": 
                    return new SmtpEmailStrategy();
                case "sendgrid": 
                    return new SendGridEmailStrategy();
                case "test": 
                    return new TestEmailStrategy();
                default: 
                    return new TestEmailStrategy();
            }
        }
    }
}