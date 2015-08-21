using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bearer.MyPrograms.SetupStrategy
{
    public class UnknownStrategy : SetupStrategyAbstract
    {
        public UnknownStrategy(SetUpDAL dbIn, string user)
            : base(dbIn, user)
        {

        }

        /// <summary>
        /// You must set these field correctly
        /// </summary>
        /// 



        public override string Memory
        {
            get
            {
                try
                {
                    throw new Exception("The strategy is unknown");
                }
                catch
                {
                    throw;
                }
            }
            set
            {
                try
                {
                    throw new Exception("The strategy is unknown");
                }
                catch
                {
                    throw;
                }
            }
        }





        /// <summary>
        /// Gets data from the data base
        /// </summary>

        public override string NameFmDb()
        {
            return SetupEnum.Unknown.ToString();
        }


        public override string Value()
        {
            throw new Exception("The strategy is unknown");
        }




        public override SetUp AddInfo(SetUp s)
        {
            try
            {
                throw new Exception("The strategy is unknown");
            }
            catch
            {
                throw;
            }

        }






    }
}