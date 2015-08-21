using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliKuli.Misc
{
    public static class WebRequests
    {
        private static string FieldName { get { return Bearer.MyPrograms.SetupStrategy.SetupEnum.CompanyName.ToString(); } }

        public static string CompanyName
        {
            get
            {
                return HttpContext.Current.Application[FieldName]!=null? HttpContext.Current.Application[FieldName].ToString():"Not Set";
            }
            set
            {
                HttpContext.Current.Application[FieldName] = value;
            }
        }

    }
}