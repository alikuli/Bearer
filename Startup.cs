<<<<<<< HEAD
﻿
=======
﻿using Bearer;
//using Bearer.Models;
>>>>>>> Persons-fork
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace Bearer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


        }
    }
}
