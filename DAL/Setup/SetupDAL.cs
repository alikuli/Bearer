using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ModelsClassLibrary.DAL.Setup
{
    public class SetupDAL : IRepositry<SetUp>
    {

        private ApplicationDbContext db;
        private string user;
        Repositry<SetUp> repo;

        public SetupDAL(ApplicationDbContext _db, string user)
        {
            this.db = _db;
            this.user = user;
            repo = new Repositry<SetUp>(db, user);
        }

        /// <summary>
        /// This simply adds. Does not save. Please add saving after this.
        /// </summary>
        /// <param name="entity"></param>

        //----------------------------------------------------------------------------------------------------------
        public void Create(SetUp entity)
        {
            var itemExists = repo.SearchFor(x => x.Name == entity.Name & x.Type == entity.Type & x.Deleted==false).FirstOrDefault();

            if (itemExists != null)
                throw new Exception(string.Format("The item '{0]' already exists! Try again."));
            try
            {
                repo.Create(entity);
            }
            catch
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------

        public SetUp FindFor(long id)
        {
            try
            {
                var item =repo.FindFor(id);
                if (item == null)
                    throw new Exception("No Item found!");

                return item;
            }
            catch
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------
        public SetUp FindFor(SetUp entity)
        {
            try
            {
                var item = repo.FindFor(entity);
                return item;
            }
            catch
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------
        public IQueryable<SetUp> FindAll()
        {
            try
            {
                var item = repo.FindAll().OrderBy(x=>x.Name);
                return item;
            }
            catch
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------
        public void Update(SetUp entity)
        {
            try
            {
                repo.Update(entity);
            }
            catch
            {
                throw;
            }
        }
        //----------------------------------------------------------------------------------------------------------

        public void Delete(long id)
        {
            try
            {
                repo.Delete(id);
            }
            catch
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------
        public void Delete(SetUp entity)
        {
            try
            {
                repo.Delete(entity);
            }
            catch
            {
                throw;
            }
        }
        //----------------------------------------------------------------------------------------------------------

        public IQueryable<SetUp> SearchFor(System.Linq.Expressions.Expression<Func<SetUp, bool>> predicate)
        {
            try
            {
                var items = repo.SearchFor(predicate);
                return items;
            }
            catch
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------

        public void Save()
        {
            try
            {
                repo.Save();
            }
            catch
            {
                throw;
            }

        }


    }
}