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
    public class ShowStartUpScreenOnStartupStrategy : SetupStrategyAbstract
    {
        public ShowStartUpScreenOnStartupStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {
            /// <summary>
            /// You must set these field correctly
            /// </summary>
            /// 
            //This is the field name
            base.fieldName = SetupEnum.ShowStartUpScreenOnStartup.ToString();

            //This is the description the item has
            base.description = "*Misc: Show Startup Screen on Start Up?";

            //This is the type of the item
            base.type = EnumTypes.Boolean;

            //This is the initial value.
            base.value = "true";

        }
    }
}