using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.SetupStrategy
{
    public class FromEmailAddressStrategy : SetupStrategyAbstract
    {
        public FromEmailAddressStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {
            base.fieldName = SetupEnum.FromEmailAddress.ToString();
            base.description = "*Email: From Email Address";
            base.type = EnumTypes.EmailAddress;
            base.value = "Enter Your FROM email Address";


        }

    }
}