using AliKuli.Exceptions;
using Bearer.Models;
using ModelsClassLibrary.DAL;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.AddressNS;
using ModelsClassLibrary.Models.CountryNS;
using ModelsClassLibrary.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AliKuli.Extentions;

namespace Bearer.DAL
{
    public class CountryDAL : Repositry<Country>
    {

        private ApplicationDbContext db;
        private string user;

        public CountryDAL(ApplicationDbContext db, string user)
            : base(db, user)
        {
            this.db = db;
            this.user = user;
        }

        public override void Create(Country entity)
        {
            //No duplicates allowed
            //search for the name
            var nameFound = base.SearchFor(x => x.Name.ToLower() == entity.Name.ToLower()).FirstOrDefault();

            if (nameFound != null)
                throw new NoDuplicateException(string.Format("The country '{0}' already exists. Try again",entity.Name));

            UpdateFieldsToTitleCaseAndUpperCase(entity);

            base.Create(entity);
        }
        public IEnumerable<SelectListItem> SelectList()
        {
            var allCountries=  base.FindAll()
                .Select(x => new SelectListItem ()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        })
                .OrderBy(x => x.Text);


            return allCountries;

        }

        public override void Update(Country entity)
        {
            UpdateFieldsToTitleCaseAndUpperCase(entity);

            base.Update(entity);
        }

        private static void UpdateFieldsToTitleCaseAndUpperCase(Country entity)
        {
            entity.Abbreviation = entity.Abbreviation.ToUpper();
            entity.Name = entity.Name.ToTitleCase();
        }
    }
}
