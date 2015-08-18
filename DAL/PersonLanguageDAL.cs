using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.CommonAndShared;
using ModelsClassLibrary.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace Bearer.DAL
{
    public class PersonLanguageDAL : Repositry<PersonLanguage>
    {

        private ApplicationDbContext db;
        private string user;


        public PersonLanguageDAL(ApplicationDbContext _db, string user)
            : base(_db, user)
        {
            this.db = _db;
            this.user = user;
        }


        public override void Create(PersonLanguage entity)
        {
            try
            {
                if(entity.PersonId!=0 && entity.LanguageId!=0)
                {
                    //we have both the required Id's. See if the PersonLanguage exists
                    PersonLanguage PersonLanguageExists = base.SearchFor(x => x.PersonId == entity.PersonId && x.LanguageId == entity.LanguageId).FirstOrDefault();
                    if (PersonLanguageExists!=null)
                        throw new AliKuli.Exceptions.NoDuplicateException();
                    //db.PersonLanguages.Attach(entity);
                    //db.Entry(entity).Collection(x => x.Languages).Load();
                    //db.Entry(entity).Collection(x => x.People).Load();

                }
                base.Create(entity);
            }
            catch
            {
                throw;
            }


            //public void Create()
        }   

    }
}