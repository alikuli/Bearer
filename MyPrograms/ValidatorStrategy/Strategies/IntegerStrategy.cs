using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.ValidatorStrategy.Strategies
{
    public class IntegerStrategy:ValidatorAbstract
    {
        public override string Validator(string item, string incoming)
        {
            int theInteger;
            bool success = int.TryParse(item, out theInteger);

            if (!success)
            {
                throw new Exception(string.Format("This: '{0}' is not an number. Please enter a non-decimal number. Try again!", item));
            }
            return theInteger.ToString();
        }
    }
}