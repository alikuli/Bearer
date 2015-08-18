using Bearer.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bearer.MyPrograms.EmailStrategy
{
    public class TestEmailStrategy: EmailStrategyAbstract
    {
        public async override Task SendAsync(IdentityMessage message, GlobalValuesVM g)
        {
            var task = new Task(() =>
            {
                string currDir = @"c:\TestEmails\";
                string nameOfEmail = Path.Combine(currDir, "email_" + message.Destination + "_" + DateTime.Now.Ticks.ToString() + ".txt");

                string[] strArray = new string[4];
                strArray[0] = "TO: " + message.Destination;
                strArray[1] = "FROM: " + g.FromEmailAddress;
                strArray[2] = "SUBJECT:" + message.Subject;
                strArray[3] = message.Body;

                File.WriteAllLines(nameOfEmail, strArray);
            });

            task.Start();
            await task;

        }

        
    }
}