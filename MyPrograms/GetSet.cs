using Bearer.MyPrograms.SetupStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AliKuli
{
    public static class GetSet
    {
        public static string Name(IPrincipal user)
        {
            return user == null ? Name(): user.Identity.Name.FirstOrDefault().ToString();
        }
        public static string Name()
        {
            return HttpContext.Current.User.Identity.Name.FirstOrDefault().ToString();
        }

        public static string CompanyName
        {
            get
            {
                return HttpContext.Current.Application[SetupEnum.CompanyName.ToString()].ToString();
            }
            set
            {
                HttpContext.Current.Application[SetupEnum.CompanyName.ToString()] = value;
            }
        }

    }
}