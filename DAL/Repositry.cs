using Bearer.Models;
using ModelsClassLibrary.DAL;
using ModelsClassLibrary.Models.CommonAndShared;
using ModelsClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bearer.DAL
{
    public abstract class Repositry<T>: IRepositry<T> where T:class, ICommon
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
        /// This adds to the Entity. Updates CreateDate to NowUTC, CreatedUser to current User,Deleted to false, Active to True. Then changes the EntityState to Added.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Create(T entity)
        {


            try
            {

                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedUser = user;
                entity.Deleted = false;
                entity.Active = true;

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
        /// This finds a record for the Entity. Checks for a zero value being passed. Then finds the record. 
        /// It defaults to non deleted records, however if you pass a true value in the 2nd parameter you can find deleted records as well.
        /// </summary>
        /// <param name="id">id, deleted=false</param>
        /// <returns>T</returns>
        public T FindFor(long id, bool deleted=false)
        {
            if (id == 0)
                throw new Exception("A zero value was passed.");

            var item = dataTable.Where(x => x.Id == id && x.Deleted == deleted).FirstOrDefault();

            if (item == null)
                throw new Exception("Repository Says: The item was not found. Try Again. ");
            
            return item;
        }

        //--------------------------------------------------------------------------------------------

        /// <summary>
        /// This finds a record for the Entity. Checks for a zero value being passed. Then finds the record. 
        /// It defaults to non deleted records, however if you pass a true value in the 2nd parameter you can find deleted records as well.
        /// </summary>
        /// <param name="id">id, deleted=false</param>
        /// <returns>T</returns>

        public T FindFor(T entity, bool deleted = false)
        {
            if(entity==null)
                throw new Exception("Repository Says: A null value was passed. Try Again.");

            return this.FindFor(entity.Id,deleted);
        }

        //--------------------------------------------------------------------------------------------
        public T FindFor(long? id, bool deleted = false)
        {
            try
            {
                return this.FindFor(long.Parse(id.ToString()), deleted);
            }
            catch
            {
                throw;
            }
        }
        //--------------------------------------------------------------------------------------------

        public async Task<T> FindForAsync(long? id, bool deleted = false)
        {
            try
            {
                return await this.FindForAsync(long.Parse(id.ToString()), deleted);
            }
            catch
            {
                throw;
            }
        }
        //--------------------------------------------------------------------------------------------

        public async Task<T> FindForAsync(long id, bool deleted = false)
        {
            if (id == 0)
                throw new Exception("A zero value was passed.");

            try
            {
                var item = await dataTable.FirstOrDefaultAsync(x => x.Id == id & x.Deleted == deleted);

                if (item == null)
                    throw new Exception("Repository Says: The record was not found.");

                return item;
            }
            catch
            {
                throw;
            }

        }

        //--------------------------------------------------------------------------------------------

        public async Task<T> FindForAsync(T entity, bool deleted = false)
        {
            if (entity == null)
                throw new Exception("A null value was passed.");
            try
            {
                return await this.FindForAsync(entity.Id, deleted);
            }
            catch
            { throw; }


        }


        //--------------------------------------------------------------------------------------------


        /// <summary>
        /// This retrieves all the values where deleted is false.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> FindAll(bool deleted=false)
        {
            try
            {
                return dataTable.Where(x => x.Deleted == deleted);

            }
            catch
            {
                throw;
            }
        }

        //--------------------------------------------------------------------------------------------


        public async Task<IList<T>> FindAllAsync(bool deleted = false)
        {
            try
            {
                return await dataTable.Where(x => x.Deleted == deleted).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        //--------------------------------------------------------------------------------------------
        /// <summary>
        /// This procedure is used to update all ICommon records
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="oldEntity"></param>
        private void Update(T entity, T oldEntity)
        {
            //e= entity
            //oe=old entity
            try
            {
                if (entity == null)
                    throw new Exception("Repository Says: A null value was passed. ");
                
                if (oldEntity == null)
                    throw new Exception("Repository Says: Previous entity not found. ");
            }
            catch
            {
                throw;
            }

            try
            {
                //all the common variables are updated
                //New data
                entity.ModifiedDate = DateTime.UtcNow;
                entity.ModifiedUser = user;

                //old Data
                entity.ModifiedDateStart = oldEntity.ModifiedDateStart;
                entity.CreatedDate = oldEntity.CreatedDate;
                entity.CreatedDateStarted = oldEntity.CreatedDateStarted;
                entity.CreatedUser = oldEntity.CreatedUser;

                //detach the oldEntity otherwise it will give problems in saving.
                db.Entry(oldEntity).State = EntityState.Detached;

                dataTable.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
            }
            catch
            {
                throw;
            }

        }

        //--------------------------------------------------------------------------------------------

        public virtual void Update(T entity)
        {
            try
            {
                T oldEntity = this.FindFor(entity.Id);
                
                this.Update(entity, oldEntity);

            }
            catch
            {
                throw;
            }


        }

        //--------------------------------------------------------------------------------------------
        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                T oldEntity = await this.FindForAsync(entity.Id);

                //This date has come from the website and so we have to add it to the new
                //data taken from the server
                oldEntity.ModifiedDateStart = entity.ModifiedDateStart;

                this.Update(entity, oldEntity);

            }
            catch
            {
                throw;
            }


        }

        //--------------------------------------------------------------------------------------------

        public virtual void Delete(long id)
        {
            try
            {
                var entity = this.FindFor(id);
                this.Delete(entity);
            }
            catch
            {
                throw;
            }


        }


        //--------------------------------------------------------------------------------------------
        
        
        public virtual void Delete(T entity)
        {

            if (entity == null)
                throw new Exception("Repository Says: A null value was passed. ");

            //We will never delete anything... we just make Delete True
            entity.Deleted = true;
            entity.DeletedByUser = user;
            entity.DeleteDate = DateTime.UtcNow;
            entity.Active = false;

            try
            {
                this.Update(entity);
            }
            catch
            {
                throw new Exception("Repository Says: An error happened when trying to Update while Deleting.");

            }
            
        }


        //--------------------------------------------------------------------------------------------



        public async Task DeleteAsync(long id)
        {
            try
            {
                var item = await this.FindForAsync(id);
                this.Delete(item);
            }
            catch
            {
                throw;
            }
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

        //--------------------------------------------------------------------------------------------

        public async Task SaveAsync()
        {
            try
            {
                await db.SaveChangesAsync();
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

            catch (InvalidOperationException)
            {
                throw new Exception("Invalid Operation Exception. Data not saved. Try again or call your systems engineer.");
            }

            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Db Update Concurrency Exception. Data not saved. Try again or call your systems engineer.");
            }

            catch (DbUpdateException)
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


        //--------------------------------------------------------------------------------------------
        /// <summary>
        /// This does not account for deleted records
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<IList<T>> SearchForAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await dataTable.Where(predicate).Where(x => x.Deleted == false).ToListAsync();
            }
            catch
            {
                throw;
            }
        }




        //--------------------------------------------------------------------------------------------
        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }





    }
}