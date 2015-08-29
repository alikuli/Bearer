using ModelsClassLibrary.Models.CommonAndShared;
using ModelsClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bearer.Models;

namespace ModelsClassLibrary.DAL
{
    public interface IRepositry<T> where T:ICommon
    {
        void Create(T entity);


        T FindFor(long id, bool deleted = false);
        T FindFor(long? id, bool deleted = false);

        Task<T> FindForAsync(long id, bool deleted = false);
        Task<T> FindForAsync(long? id, bool deleted = false);


        T FindFor(T entity, bool deleted=false);
        Task<T> FindForAsync(T entity, bool deleted=false);


        IQueryable<T> FindAll(bool deleted=false);
        Task<IList<T>> FindAllAsync(bool deleted=false);



        void Update(T entity);
        Task UpdateAsync(T entity);


        void Delete(long id );
        Task DeleteAsync(long id);


        void Delete(T entity);
        //Task DeleteAsync(T entity);


        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        Task<IList<T>> SearchForAsync(Expression<Func<T, bool>> predicate);

        ApplicationDbContext GetDb { get;}
        string GetUser { get; }

        //SelectList SelectList(T entity);
        //Task <SelectList> SelectListAsync(T entity);

        void Save();
        Task SaveAsync();

        void Dispose();

    }
}
