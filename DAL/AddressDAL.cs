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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bearer.DAL
{
    public class AddressDAL : Repositry<Address>
    {

        private ApplicationDbContext db;
        private string user;

        public AddressDAL(ApplicationDbContext db, string user)
            : base(db, user)
        {
            this.db = db;
            this.user = user;
        }

        public IEnumerable<SelectListItem> SelectList()
        {
            var allAddresses = base.FindAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .OrderBy(x => x.Text);


            return allAddresses;

        }


        public IEnumerable<SelectListItem> SelectListCountries()
        {
            CountryDAL countryDAL = new CountryDAL(db, user);
            return countryDAL.SelectList();
        }


        public override void Update(Address entity)
        {
            FindCountry(entity);
            base.Update(entity);
        }

        public override async Task UpdateAsync(Address entity)
        {
            FindCountry(entity);

            await base.UpdateAsync(entity);
        }

        private void FindCountry(Address entity)
        {
            long theId = long.Parse(entity.CountryID.ToString());

            if (entity.CountryID >0)
            {
                try
                {

                    //Get the country and add it
                    CountryDAL cDAL = new CountryDAL(db, user);
                    Country c = cDAL.SearchFor(x => x.Id == theId).FirstOrDefault();

                    entity.Country = c;
                }
                catch(Exception e)
                {
                    string error = e.Message;
                    throw;
                }
            }
        }


        public override void Create(Address entity)
        {
            FindCountry(entity);

            base.Create(entity);
        }

        public override IQueryable<Address> FindAll(bool deleted = false)
        {
            return base.FindAll(deleted).OrderBy(x=>x.Name);
        }


        public override async Task<IList<Address>> FindAllAsync(bool deleted = false)
        {
            return (await base.FindAllAsync(deleted)).OrderBy(x=>x.Name).ToList();
        }

    }
}
