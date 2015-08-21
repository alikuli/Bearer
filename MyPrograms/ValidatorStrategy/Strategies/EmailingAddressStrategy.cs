using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.ValidatorStrategy.Strategies
{
    public class EmailingAddressStrategy : ValidatorAbstract
    {

        /// <summary>
        /// This one has no return. Just an empty string;
        /// </summary>
        /// <param name="item"></param>
        /// <param name="incomingField"></param>
        /// <returns></returns>
        public override string Validator(string item, string incomingField)
        {
            try
            {
                if (!AliKuli.Validators.MyValidators.IsValidEmail(item))
                    throw new Exception(string.Format("The '{0}' is not a valid email address",incomingField));
            }
            catch
            {
                throw;
            }

            return "";
        }
    }
}