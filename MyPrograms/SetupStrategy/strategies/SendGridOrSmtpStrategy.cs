﻿using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.SetupStrategy.strategies
{
    public class SendGridOrSmtpStrategy:SetupStrategyAbstract
    {
        public SendGridOrSmtpStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {
            /// <summary>
            /// You must set these field correctly
            /// </summary>
            /// 
            //This is the field name
            base.fieldName = SetupEnum.SendGridOrSmtp.ToString();

            //This is the description the item has
            base.description = "*Email: Use SMTP or Sendgrid";

            //This is the type of the item
            base.type = EnumTypes.EmailingMethod;

            //This is the initial value.
            base.value = "SMTP";


        }

    }
}