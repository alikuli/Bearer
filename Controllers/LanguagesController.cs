using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.DAL;
using ModelsClassLibrary.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AliKuli.Exceptions;


namespace Bearer.Controllers
{
    public class LanguagesController : AbstractController<Language>
    {
        //public  LanguageDAL lDal;
        public LanguagesController()
            : base(new LanguageDAL(SetApplicationDbContext(), GetUser()))
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
            string s = AliKuli.GetUser.Name(null);
            sb.Append(s);
            return s.ToString();
        }

        public override async Task<ActionResult> Index (string message = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("", message);
            }

            try
            {
                var t = await repo.FindAllAsync();
                return View(t.OrderBy(x=>x.Name));

            }
            catch (Exception e)
            {
                MakeErrorMesage("Something went wrong. Try again.", e);
                return View(repo.FindAll().OrderBy(x => x.Name));
            }

        }
    }
}