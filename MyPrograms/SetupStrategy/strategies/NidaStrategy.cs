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
    public class NidaStrategy : SetupStrategyAbstract
    {
        public NidaStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {
            //This is the field name
            base.fieldName = SetupEnum.Nida.ToString();

            //This is the description the item has
            base.description = "Nida: For Testomg";

            //This is the type of the item
            base.type = EnumTypes.String;

            //This is the initial value.
            base.value = "Nida Test";

        }




    }
}