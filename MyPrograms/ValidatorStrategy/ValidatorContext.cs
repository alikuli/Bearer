using Bearer.MyPrograms.ValidatorStrategy.Strategies;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.ValidatorStrategy
{
    public static class ValidatorContext
    {
        public static IValidator Validate (EnumTypes type)
        {
            switch(type)
            {
                case EnumTypes.Boolean: return new BooleanStrategy();
                case EnumTypes.Integer: return new IntegerStrategy();
                case EnumTypes.EmailingMethod: return new EmailingMethodStrategy();
                case EnumTypes.EmailAddress: return new EmailingAddressStrategy();
                case EnumTypes.FilePath: return new FilePathStrategy();
                case EnumTypes.String: return new StringStrategy();

                default: return new UnknownStrategy();
            }
        }
    }
}