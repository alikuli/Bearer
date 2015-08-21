using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.AddressNS;
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
            string s = AliKuli.GetSet.Name(null);
            sb.Append(s);
            return s.ToString();
        }

        public override async System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Index(string message = "")
        {
            //We need the select list from Address
            Repositry<Address> addressRep = new AddressDAL(base.repo.GetDb, base.repo.GetUser);
            AddressDAL addyDAL = (AddressDAL)addressRep;
            ViewBag.AddressSelectList = addyDAL.SelectList();

            return await base.Index(message);
        }   
    }
}