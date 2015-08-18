using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.DAL;
using ModelsClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bearer.Controllers
{
    interface IController<T> where T:ICommon
    {
        
        // GET: Languages
        Task<ActionResult> Index(string message = "");


        // GET: Languages/Details/5
        Task<ActionResult> Details(long? id);

        // GET: Languages/Create
        ActionResult Create();


        
        // POST: Languages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        Task<ActionResult> Create([Bind(Include = "Name,Comment,CreatedDateStarted,")] T entity);

        
        
        //----------------------------------------------------------------

        
        
        
        
        // GET: Languages/Edit/5
        Task<ActionResult> Edit(long? id);



        //----------------------------------------------------------------






        // POST: Languages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        Task<ActionResult> Edit([Bind(Include = "Id,Name,Comment,ModifiedDateStart")] T entity);



        //----------------------------------------------------------------




        // GET: Languages/Delete/5
         Task<ActionResult> Delete(long? id, string message = "");



        //----------------------------------------------------------------




        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        Task<ActionResult> DeleteConfirmed(long id);


        //----------------------------------------------------------------


        //void Dispose(bool disposing);
    }

}

