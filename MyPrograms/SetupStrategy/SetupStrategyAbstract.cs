using Bearer.DAL;
using Bearer.Models;
using Bearer.MyPrograms.ValidatorStrategy;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.SetupStrategy
{
	public class SetupStrategyAbstract:ISetupStrategy
	{

		protected ApplicationDbContext db;
		protected SetUpDAL repo;

        protected string fieldName;

        //This is the description the item has
        protected string description;

        //This is the type of the item
        protected EnumTypes type;

        //This is the initial value.
        protected string value;



        public SetupStrategyAbstract(SetUpDAL repo, string user)
		{
            this.repo = repo;
		}



        public  virtual SetUp AddInfo(SetUp s)
        {
            s.Name = fieldName;

            s.Description = description;
            s.Type = type;
            s.Value = value;
            return s;

        }

        public virtual string NameFmDb()
        {
            return repo.SearchFor(x => x.Name == fieldName).FirstOrDefault()!= null ?
                repo.SearchFor(x => x.Name == fieldName).FirstOrDefault().Name.ToString() :
                string.Empty;
        }
        public virtual string ValueDb()
        {
            return repo.SearchFor(x => x.Name == fieldName).FirstOrDefault().Value != null ?
                repo.SearchFor(x => x.Name == fieldName).FirstOrDefault().Value.ToString() :
                string.Empty;
        }




        public virtual string Memory
        {
            get
            {
                return HttpContext.Current.Application[fieldName].ToString();
            }
            set
            {
                HttpContext.Current.Application[fieldName] = value;
            }
        }

        public virtual string Validate(SetUp s)
        {
            value = s.Value;
            if (!s.Value.ToLower().Equals(value.ToLower()))
                return ValidatorContext.Validate(type).Validator(value, fieldName);
            else return value;
        }
    }
}