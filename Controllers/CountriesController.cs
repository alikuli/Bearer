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
using ModelsClassLibrary.Models.CountryNS;
using Bearer.DAL;
using System.Text;

namespace Bearer.Controllers
{
    public class CountriesController : AbstractController<Country>
    {
        //public  LanguageDAL lDal;
        public CountriesController()
            : base(new CountryDAL(SetApplicationDbContext(), GetUser()))
        {

        }


        //private static LanguageDAL SetLanguageDal()
        //{
        //    lDal = ;
        //    return lDal;

        //}

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

        public override async Task<ActionResult> Index(string message = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("", message);
            }

            try
            {
                var t = await repo.FindAllAsync();
                return View(t.OrderBy(x => x.Name));

            }
            catch (Exception e)
            {
                MakeErrorMesage("Something went wrong. Try again.", e);
                return View(repo.FindAll().OrderBy(x => x.Name));
            }

        }
    }
}
