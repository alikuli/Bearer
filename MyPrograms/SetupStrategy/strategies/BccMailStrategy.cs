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
    public class BccMailStrategy : SetupStrategyAbstract
    {
        public BccMailStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {
            //This is the field name
            base.fieldName = SetupEnum.BccEmailAddress.ToString();

            //This is the description the item has
            base.description = "Email: BCC Email Address -Address where you receive blind emails eg. Login emails";

            //This is the type of the item
            base.type = EnumTypes.String;

            //This is the initial value.
            base.value = "";

        }




    }
}