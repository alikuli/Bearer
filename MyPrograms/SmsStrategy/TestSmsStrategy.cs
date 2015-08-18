using Bearer.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bearer.MyPrograms.SmsStrategy
{
    public class TestSmsStrategy:ISmsStrategy
    {

        public Task SendAsync(IdentityMessage message, GlobalValuesVM g)
        {
            var task = new Task(() =>
            {
                string currDir = @"c:\TestSms\";
                string nameOfEmail = Path.Combine(currDir, "sms_" + message.Destination + "_" + DateTime.Now.Ticks.ToString() + ".txt");

                string[] strArray = new string[3];
                strArray[0] = "TO: " + message.Destination;
                strArray[1] = "FROM: " + g.FromEmailAddress;
                strArray[2] = message.Body;
                try
                {
                    File.WriteAllLines(nameOfEmail, strArray);
                }
                catch
                {
                    throw;
                }
            });

            task.Start();
            //a blank
            return Task.FromResult(0);
        }
    }
}