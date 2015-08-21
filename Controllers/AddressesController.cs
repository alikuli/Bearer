using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bearer.Models;
using ModelsClassLibrary.Models.AddressNS;
using Bearer.DAL;
using System.Text;

namespace Bearer.Controllers
{
    public class AddressesController : AbstractController<ModelsClassLibrary.Models.AddressNS.Address>
    {
        public AddressesController() :
            base(new AddressDAL(SetApplicationDbContext(), GetUser()))
        {

        }
        private static ApplicationDbContext SetApplicationDbContext()
        {
            return new ApplicationDbContext();
        }

        private static string GetUser()
        {
            StringBuilder sb = new StringBuilder();
            string s = AliKuli.GetSet.Name(null);
            sb.Append(s);
            return s.ToString();
        }

        //---------------------------------------------------------------------

    }
}
