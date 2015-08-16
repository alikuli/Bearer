using Bearer.Models;
using ModelsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.DAL
{
    public class SetUpDAL:Repositry<SetUp>
    {

        private ApplicationDbContext db;
        private string user;

        public SetUpDAL(ApplicationDbContext db, string user):base(db,user)
        {
            this.db = db;
            this.user = user;
        }

        public override void Create(SetUp entity)
        {

            //Dont allow duplicates
            var itemExists = this.SearchFor(x => x.Name == entity.Name & x.Type == entity.Type & x.Deleted == false).FirstOrDefault();

            if (itemExists != null)
                throw new Exception(string.Format("The item '{0]' already exists! Try again."));

            try
            {
                base.Create(entity);
            }
            catch
            { 
                throw;
            }
        }
    }
}