using Bearer.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bearer.MyPrograms.SmsStrategy
{
    public interface ISmsStrategy
    {

        Task SendAsync(IdentityMessage message, GlobalValuesVM g);
    }
}