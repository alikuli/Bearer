﻿using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.SetupStrategy
{
    public class SendGridPasswordStrategy : SetupStrategyAbstract
    {
        public SendGridPasswordStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {
            /// <summary>
            /// You must set these field correctly
            /// </summary>
            /// 
            //This is the field name
            base.fieldName = SetupEnum.CompanyName.ToString();

            //This is the description the item has
            base.description = "Email SendGrid: sendgrid.com Password ";

            //This is the type of the item
            base.type = EnumTypes.String;

            //This is the initial value.
            base.value = "Enter Your sendgrid.com Password";

        }

    }
}