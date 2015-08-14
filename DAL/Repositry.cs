using Bearer.Models;
using ModelsClassLibrary.DAL;
using ModelsClassLibrary.Models.CommonAndShared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Bearer.DAL
{
    public class Repositry<T>: IRepositry<T> where T:class, IEntity
    {
        private ApplicationDbContext db;
        private DbSet<T> dataTable;
        private string user;
        //--------------------------------------------------------------------------------------------

        public Repositry(ApplicationDbContext db, string user)
        {
            this.db = db;
            dataTable = this.db.Set<T>();
            this.user = user;
        }

        //--------------------------------------------------------------------------------------------

        /// <summary>
        /// This simply adds and sets the EntityState to Added
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Create(T entity)
        {
            
            try
            {
                dataTable.Add(entity);
                db.Entry(entity).State = EntityState.Added;
            }
            catch
            { 
                throw; 
            }
        }

        //--------------------------------------------------------------------------------------------

        /// <summary>
        /// This finds a record for the Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindFor(long id)
        {
            if (id == 0)
                throw new Exception("A zero value was passed.");

            var item = dataTable.Where(x => x.Id==id && x.Deleted==false).FirstOrDefault();

            if (item == null)
                throw new Exception("Repository Says: The item was not found. Try Again. ");
            
            return item;
        }

        //--------------------------------------------------------------------------------------------

        /// <summary>
        /// This uses the FindFor(Id) method. It throws an exception if a null entity is passed.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        public T FindFor(T entity)
        {
            if(entity==null)
                throw new Exception("Repository Says: A null value was passed. Try Again.");

            return this.FindFor(entity.Id);
        }

        //--------------------------------------------------------------------------------------------


        /// <summary>
        /// This retrieves all the values where deleted is false.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> FindAll()
        {
            return dataTable.Where(x => x.Deleted == false);
        }

        //--------------------------------------------------------------------------------------------

        
        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new Exception("Repository Says: A null value was passed. ");

            try
            {
                dataTable.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
            }
            catch
            {
                throw new Exception("Repository Says: An error happened when trying to Update. Try Again. ");
            }

        }

        //--------------------------------------------------------------------------------------------

        public virtual void Delete(long id)
        {
            var entity = this.FindFor(id);
            
            //We will never delete anything... we just make Delete True
            entity.Deleted = true;
            entity.DeletedByUser = user;
            entity.DeleteDate = DateTime.UtcNow;

            try
            {
                this.Update(entity);
            }
            catch
            {
                throw new Exception("Repository Says: An error happened when trying to Update while Deleting. Try Again. ");

            }


        }


        //--------------------------------------------------------------------------------------------
        
        
        public virtual void Delete(T entity)
        {

            if (entity == null)
                throw new Exception("Repository Says: A null value was passed. ");

            this.Delete(entity.Id);
        }


        //--------------------------------------------------------------------------------------------

        public virtual IQueryable<T> SearchFor(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return dataTable.Where(predicate).Where(x=>x.Deleted==false);
        }

        //--------------------------------------------------------------------------------------------


        public virtual void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException)
            {
                throw new Exception("Db Entity Validation Exception. Data not saved. Try again or call your systems engineer.");
            }

            catch (NotSupportedException)
            {

                throw new Exception("Not supported Exception. Data not saved. Try again or call your systems engineer.");
            }


            catch (ObjectDisposedException)
            {

                throw new Exception("Object Disposed Exception. Data not saved. Try again or call your systems engineer.");

            }

            catch (InvalidOperationException )
            {
                throw new Exception("Invalid Operation Exception. Data not saved. Try again or call your systems engineer.");
            }

            catch (DbUpdateConcurrencyException )
            {
                throw new Exception("Db Update Concurrency Exception. Data not saved. Try again or call your systems engineer.");
            }

            catch (DbUpdateException )
            {
                throw new Exception("Db Update Exception. Data not saved. Try again or call your systems engineer.");
            }

            catch (EntityException)
            {
                throw new Exception("Entity Exception. Data not saved. Try again or call your systems engineer.");
            }

            catch (DataException)
            {
                throw new Exception("Data Exception. Data not saved. Try again or call your systems engineer.");
            }

            catch (Exception)
            {
                throw new Exception("General Exception. Data not saved. Try again or call your systems engineer.");

            }
        }




    }
}