using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.ValidatorStrategy.Strategies
{
    public class StringStrategy:ValidatorAbstract
    {
        public override string Validator(string item, string incoming)
        {
            //do nothing
            return item;
        }
    }
}