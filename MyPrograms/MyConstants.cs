using AppDbx.Models;
//using Bearer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliKuli
{
    public static class MyConstants
    {
        public  const string AdminConst = "Administrator";

        public  const string defaultPageSize = "10";

        public static int DefaultPageSizeFromSetup(ApplicationDbContext db)
        {
            GlobalValuesVM globalValues = new GlobalValuesVM(db);

            string theDefaultPageSize = globalValues.DefaultPageSize;

            int defaultPageSizeFromSetup;
            bool success = int.TryParse(theDefaultPageSize, out defaultPageSizeFromSetup);

            if (success)
            {
                return defaultPageSizeFromSetup;
            }
            else
                return int.Parse(defaultPageSize);
            
        }
    }
}