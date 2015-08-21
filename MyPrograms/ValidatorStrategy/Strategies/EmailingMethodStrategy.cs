using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.ValidatorStrategy.Strategies
{
    public class EmailingMethodStrategy : ValidatorAbstract
    {
        public override string Validator(string item, string incomingField)
        {
            switch (item.ToLower())
            {
                case "smtp": return item;
                case "sendgrid": return item;
                case "test": return item;
                default: return "test";
            }

            ;
        }
    }
}