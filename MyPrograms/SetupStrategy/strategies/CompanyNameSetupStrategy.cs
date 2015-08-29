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
    public class CompanyNameSetupStrategy:SetupStrategyAbstract
    {
        public CompanyNameSetupStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {
            //This is the field name
            base.fieldName = SetupEnum.CompanyName.ToString();

            //This is the description the item has
            base.description = "Misc: Company Name";

            //This is the type of the item
            base.type = EnumTypes.String;

            string theValue = ValueDb();

            if (string.IsNullOrEmpty(theValue))
                base.value = "Sample Company";
            else
                base.value = theValue;

            this.Memory = theValue;

        }

        public override SetUp AddInfo(SetUp s)
        {   
            return base.AddInfo(s);
        }


    }
}