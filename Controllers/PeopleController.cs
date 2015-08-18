using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Bearer.Controllers
{
    public class PeopleController:AbstractController<Person>
    {
        public PeopleController()
            : base(new PersonDAL(SetApplicationDbContext(), GetUser()))
        {

        }

        private static ApplicationDbContext SetApplicationDbContext()
        {
            return new ApplicationDbContext();
        }

        private static string GetUser()
        {
            StringBuilder sb = new StringBuilder();
            string s = AliKuli.GetUser.Name(null);
            sb.Append(s);
            return s.ToString();
        }

        
    }
}