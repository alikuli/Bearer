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
    public class DefaultPageSizeStrategy : SetupStrategyAbstract
    {
        public DefaultPageSizeStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {
            //This is the field name
            base.fieldName = SetupEnum.DefaultPageSize.ToString();

            //This is the description the item has
            base.description = "*Misc: Default No of Records per screen";

            //This is the type of the item
            base.type = EnumTypes.Integer;

            //This is the initial value.
            base.value = "10";

        }

        /// <summary>
        /// You must set these field correctly
        /// </summary>
        /// 

    }
}