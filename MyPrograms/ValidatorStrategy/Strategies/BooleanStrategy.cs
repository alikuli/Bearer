using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.ValidatorStrategy.Strategies
{
    public class BooleanStrategy : ValidatorAbstract
    {
        public override string Validator(string currValue, string incomingField)
        {
            if (currValue.Trim().ToLower() == "yes" ||
                currValue.Trim().ToLower() == "true" ||
                currValue.Trim().ToLower() == "no" ||
                currValue.Trim().ToLower() == "false")
            {
            }
            else
            {
                string message= string.Format( "Your answer must be 'Yes' or 'No' or 'True' or 'False' for {0} Try again!",incomingField);
                throw new Exception(message);

            }


            if (currValue.Trim().ToLower() == "yes" || currValue.Trim().ToLower() == "true")
            {
                return "true";
            }

            if (currValue.Trim().ToLower() == "no" ||
                currValue.Trim().ToLower() == "false")
            {
                return "false";
            }

            return "false";
            
        }
    }
}