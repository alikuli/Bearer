using AliKuli.Exceptions;
using Bearer.Models;
using Bearer.MyPrograms.SetupStrategy;
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




            //Get the company name
            //Check to see if the company is coming in
            //if (entity.Name==AliKuli.MyConstants.SetupMyConstant.CompanyNameField)
            //    AliKuli.GlobalSetupValues.CompanyName = itemExists.Name;

            try
            {
                //Dont allow duplicates
                var itemExists = this.SearchFor(x => x.Name == entity.Name & x.Type == entity.Type).FirstOrDefault();
                if (itemExists != null)
                    throw new NoDuplicateException(string.Format("The item '{0}' already exists! Try again.",entity.Name));
                
                AliKuli.Misc.WebRequests.CompanyName = entity.Name;

                base.Create(entity);
            }
            catch
            { 
                throw;
            }
        }

        public void DeleteAll()
        {
            try
            {
                //we have to add ToList otherwise we get an error that 2 readers are open.
                var aliveSetup = this.FindAll().ToList();
                if (aliveSetup != null)
                {
                    foreach (var item in aliveSetup)
                    {
                        this.Delete(item);
                    }
                    this.Save();
                }

            }
            catch (Exception e)
            {
                throw new Exception("There was a problem while deleting. ", e);

            }
        }

        /// <summary>
        /// This initializes the entire setup.
        /// </summary>
        public void InitializeSetUp()
        {

            //Add All missing

            try
            {
                foreach (string item in Enum.GetNames(typeof(SetupEnum)))
                {
                    //Create the context that will select the Setup type
                    var setupContext = new SetupContext(this, user);

                    //Select the setuptype
                    ISetupStrategy setupStrategy = setupContext.Create(item);

                    //this does not work becaz

                    Type theType = typeof(SetupEnum);

                    if (string.Equals(
                            item,
                            Enum.GetName(theType, SetupEnum.Unknown),
                            StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    //Create a new setup
                    SetUp s = new SetUp();

                    //Add the info to the setup from the setupT
                    s = setupStrategy.AddInfo(s);
                    setupStrategy.Validate(s);
                    try
                    {
                        this.Create(s);
                        this.Save();

                    }
                    catch (NoDuplicateException)
                    {
                        //Do nothing
                    }

                    catch (Exception )
                    {
                        throw;
                    }
                }

            }
            catch
            {
                throw;
            }
        }

        public override void Update(SetUp entity)
        {
            try
            {

                base.Update(entity);

                //if (entity.Name == SetupEnum.CompanyName.ToString())
                //    AliKuli.Misc.WebRequests.CompanyName = entity.Name;
            }
            catch { throw; }
        }

        

    }
}