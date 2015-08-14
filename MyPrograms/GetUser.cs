using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AliKuli
{
    public static class GetUser
    {
        public static string Name(IPrincipal user)
        {
            return user==null? HttpContext.Current.User.Identity.Name:user.Identity.Name;
        }
    }
}