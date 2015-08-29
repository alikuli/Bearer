using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.ValidatorStrategy
{
    public abstract class ValidatorAbstract : Bearer.MyPrograms.ValidatorStrategy.IValidator
    {
        public virtual string Validator(string CurrValue,string incomingField)
        {
            try
            {
                throw new Exception("Unknown Validator");
            }
            catch
            {
                throw;
            }
        }


    }
}