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
    public class PersonDAL : Repositry<Person>
    {

        private ApplicationDbContext db;
        private string user;
        

        public PersonDAL(ApplicationDbContext _db, string user):base(_db,user)
        {
            this.db = _db;
            this.user = user;
        }

        public override void Create(Person entity)
        {
            try
            {
                //We dont want duplicates i.e. same CNIC/IdentificationNo number
                var foundE = SearchFor(x=>x.IdentificationNo==entity.IdentificationNo).FirstOrDefault();

                if(foundE != null)
                {

                    //dont allow black listed to get entered again.
                    if (foundE.BlackListed)
                    {
                        throw new Exception(string.Format("This user '{0}' exists as '{1}' in db and was BLACKLISTED on '{2}' by '{3}.' Comment reads '{4}'",
                            entity.FullName,
                            foundE.FullName,
                            foundE.ModifiedDate.ToString(),
                            foundE.ModifiedUser,
                            foundE.Comment));

                    }

                    //other, Active.
                    throw new Exception(string.Format("CNIC '{5}' for '{0}.' This user exists in the db as '{1}'. Current ACTIVE Status is: '{2}'. The Comment reads '{3}'. Modified Date: '{4}'. You cannot re-enter it. Reactivate. If name is different then, possible FRAUD.",
                        entity.FullName,
                        foundE.FullName,
                        foundE.Active.ToString(),
                        foundE.Comment,
                        foundE.ModifiedDate.ToString() ?? "NONE",
                        entity.IdentificationNo));

                }


                //Sex needs to be identified... cant have an unknown.
                if (entity.Sex==SexEnum.Unknown)
                {
                    throw new Exception(string.Format("You have not entered the sex for user '{0}'. Please enter the sex. (Male or Female)",entity.FullName));

                }



                //We want to know the father's name
                if (entity.SonOfOrWifeOf== SonOfWifeOfDotOfEnum.Unknown)
                {
                    throw new Exception(string.Format("You have not entered the parentage/husband for '{0}'. Please enter the parentage or Husband. (W/O, S/O. D/O)", entity.FullName));

                }

                base.Create(entity);
            }
            catch
            {
                throw;
            }
        }
        
    }
}