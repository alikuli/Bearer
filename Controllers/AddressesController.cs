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
using ModelsClassLibrary.Models.AddressNS;

namespace Bearer.Controllers
{
    public class AddressesController : AbstractController<Address>
    {

        private static ApplicationDbContext _db;
        private static string _user;
        
        
        //public  LanguageDAL lDal;
        public AddressesController()
            : base(new AddressDAL(SetApplicationDbContext(), GetUser()))
        {

        }


        private static ApplicationDbContext SetApplicationDbContext()
        {
            _db = new ApplicationDbContext();
            return _db;
        }

        private static string GetUser()
        {
            StringBuilder sb = new StringBuilder();
            string s = AliKuli.GetSet.Name(null);
            sb.Append(s);
            _user = s.ToString();
            return _user;
        }


        //====================================================================


        

        public override ActionResult Create()
        {
            CountryDAL cDal = new CountryDAL(_db, _user);
            ViewBag.Countries = cDal.SelectList();

            return base.Create();
        }






        public override async Task<ActionResult> Edit([Bind(Include = "Id,Name,Comment,ModifiedDateStart,PhoneIntlCode,Abbreviation,HouseNo,Road,Address2,City,State,Zip,CountryID")] Address entity)
        {
            CountryDAL cDal = new CountryDAL(_db, _user);
            ViewBag.Countries = cDal.SelectList();

            return await base.Edit(entity);
        }

        public override async Task<ActionResult> Edit(long? id)
        {
            CountryDAL cDal = new CountryDAL(_db, _user);
            ViewBag.Countries = cDal.SelectList();

            return await base.Edit(id);
        }

    }
}
