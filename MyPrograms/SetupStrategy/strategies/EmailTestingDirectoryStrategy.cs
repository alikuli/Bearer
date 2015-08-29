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
    public class EmailTestingDirectoryStrategy : SetupStrategyAbstract
    {
        public EmailTestingDirectoryStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {
            //This is the field name
            base.fieldName = SetupEnum.EmailTestingDirectory.ToString();

            //This is the description the item has
            base.description = "Email Testing Directory: Test emails land here.";

            //This is the type of the item
            base.type = EnumTypes.FilePath;

            //This is the initial value.
            base.value = @"c:\TestEmails\"; 

        }

    }
}