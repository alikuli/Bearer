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
    public class IsSendBccStrategy : SetupStrategyAbstract
    {
        public IsSendBccStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {
            /// <summary>
            /// You must set these field correctly
            /// </summary>
            /// 
            //This is the field name
            base.fieldName = SetupEnum.IsSendBcc.ToString();

            //This is the description the item has
            base.description = "*Email: Do you want BCC messages?";

            //This is the type of the item
            base.type = EnumTypes.Boolean;

            //This is the initial value.
            base.value = "true";

        }
    }
}