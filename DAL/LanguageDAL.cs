using AliKuli.Exceptions;
using Bearer.Models;
using ModelsClassLibrary.DAL;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.DAL
{
    public class LanguageDAL:Repositry<Language>
    {

        private ApplicationDbContext db;
        private string user;

        public LanguageDAL(ApplicationDbContext db, string user)
            : base(db, user)
        {
            this.db = db;
            this.user = user;
        }


        public override void Create(Language entity)
        {
            var found = this.SearchFor(x => x.Name == entity.Name).FirstOrDefault();

            try
            {
                if (found !=null)
                    throw new NoDuplicateException();
                base.Create(entity);
            }
            catch
            {
                throw;
            }
        }

        public override void Delete(Language entity)
        {

            try
            {
                //Make sure that no Person is using this
                IRepositry<PersonLanguage> plDAL = new PersonLanguageDAL(db, user);
                IRepositry<Person> pDAL = new PersonDAL(db, user);

                var personExists = plDAL.SearchFor(x => x.LanguageId == entity.Id);
                if (personExists.Count() > 0)
                {
                    string existingPeople = "";
                    foreach (var item in personExists)
                    {

                        string personNameAndId = pDAL.FindFor(item.PersonId) != null ?
                            pDAL.FindFor(item.PersonId).FullNameWithId :
                            string.Empty;

                        if (!string.IsNullOrEmpty(personNameAndId))
                            existingPeople += ", " + personNameAndId;
                    }

                    existingPeople += ".";
                    existingPeople=existingPeople.Substring(2);
                    throw new PersonExistsException(existingPeople);
                }
                base.Delete(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
