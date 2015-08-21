using AliKuli.Exceptions;
using Bearer.Models;
using ModelsClassLibrary.DAL;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.AddressNS;
using ModelsClassLibrary.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public SelectList SelectList()
        {
            return (SelectList)base.FindAll()
                .Select(x => new
                        {
                            Text = x.Name,
                            Value = x.Id
                        })
                .OrderBy(x => x.Text);

        }

    }
}
