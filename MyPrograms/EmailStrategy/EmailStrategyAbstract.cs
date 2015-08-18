using Bearer.Models;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bearer.MyPrograms.EmailStrategy
{
    public abstract class EmailStrategyAbstract : IEmailStrategy
    {

        public  virtual Task SendAsync(IdentityMessage message, GlobalValuesVM g)
        {
            try
            {


                throw new NotImplementedException();
            }
            catch { throw; }
        }
    }
}