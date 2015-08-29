namespace Bearer.Migrations
{
    using Bearer.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bearer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Bearer.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Customers.AddOrUpdate(
            //    c=>c.Name,
            //    //1
            //    new Customer
            //    {
            //        Name="Ali Kuli Aminuddin",
            //        Address1="Gulkali",
            //        Address2 = "Main Harbanspura Road",
            //        City="Lahore",
            //        State="Punjab",
            //        Country="Pakistan",
            //        MobilePhone="0331 4474 120",
            //        Phone="042 1234 5555",
            //        Email="ali@ultimateDistribution.net",
            //        CreatedUser= "AKA",
            //        ModifiedUser= "AA",
            //        ModifiedDate=new DateTimeAdapter().UtcNow,
            //        Comment="This is a test from config."

            //    },

            //    //2
            //    new Customer
            //    {
            //        Name="Nurjhan Khattak",
            //        Address1="2, The Mall",
            //        City="Peshawar",
            //        State="NWFP",
            //        Country="Pakistan",
            //        MobilePhone="0300 948 2392",
            //        Phone="051 123 45678",
            //        Email="enjay@Hotmail.com",
            //        CreatedUser= "NJ",
            //        ModifiedUser= "ABA",
            //        ModifiedDate=new DateTimeAdapter().UtcNow,
            //        Comment="This is a 2nd customer test from config."

            //    },

            //    //3
            //    new Customer
            //    {
            //        Name="Nida Ali Kuli",
            //        Address1="Gulkali",
            //        Address2 = "Main Harbanspura Road",
            //        City="Lahore",
            //        State="Punjab",
            //        Country="Pakistan",
            //        MobilePhone="0331 4474 120",
            //        Phone="042 1234 4444",
            //        Email="Nida@ultimateDistribution.net",
            //        CreatedUser= "AKA",
            //        ModifiedUser= "AA",
            //        ModifiedDate=DateTime.UtcNow,
            //        Comment="This is a test from config."

            //    },

            //    //4
            //    new Customer
            //    {
            //        Name="Erum Bhatti",
            //        Address1="Wapda Town",
            //        Address2 = "Link Road",
            //        City="Lahore",
            //        State="Punjab",
            //        Country="Pakistan",
            //        MobilePhone="0322 4344 120",
            //        Phone="042 1234 5555",
            //        Email="Erum@ultimateDistribution.net",
            //        CreatedUser= "AKA",
            //        ModifiedUser= "AA",
            //        ModifiedDate=DateTime.UtcNow,
            //        Comment="This is a test from config."

            //    },

            //    //5
            //    new Customer
            //    {
            //        Name="Alia Azhar",
            //        Address1="Gulkali",
            //        Address2 = "Main Harbanspura Road",
            //        City="Lahore",
            //        State="Punjab",
            //        Country="Pakistan",
            //        MobilePhone="0300 234 5543",
            //        Phone="042 1234 5555",
            //        Email="ali@ultimateDistribution.net",
            //        CreatedUser= "AKA",
            //        ModifiedUser= "AA",
            //        ModifiedDate=DateTime.UtcNow,
            //        Comment="This is a test from config."

            //    }


            //    );

            //context.Ips.AddOrUpdate(
            //    a=>a.FromIP,
            //    new IP
            //    {
            //        FromIP="192.168.15.01",
            //        ToIP = "192.168.15.15",
            //        CustomerId=1,

            //        CreatedUser= "NJ",
            //        ModifiedUser= "ABA",
            //        ModifiedDate=DateTime.UtcNow,
            //        Comment="This is the IP."
            //    },
            //    new IP
            //    {
            //        FromIP="192.168.15.16",
            //        ToIP = "192.168.15.25",
            //        CustomerId = 1,

            //        CreatedUser = "NJ",
            //        ModifiedUser = "ABA",
            //        ModifiedDate = DateTime.UtcNow,
            //        Comment = "This is the IP."
            //    },
            //    new IP
            //    {
            //        FromIP="192.168.15.26",
            //        ToIP = "192.168.15.29",
            //        CustomerId = 1,

            //        CreatedUser = "NJ",
            //        ModifiedUser = "ABA",
            //        ModifiedDate = DateTime.UtcNow,
            //        Comment = "This is the IP."
            //    },

            //    new IP
            //    {
            //        FromIP="193.168.15.01",
            //        ToIP = "193.168.15.15",
            //        CustomerId = 2,

            //        CreatedUser = "NJ",
            //        ModifiedUser = "ABA",
            //        ModifiedDate = DateTime.UtcNow,
            //        Comment = "This is the IP."
            //    },
            //    new IP
            //    {
            //        FromIP="193.168.15.16",
            //        ToIP = "193.168.15.25",
            //        CustomerId = 2,

            //        CreatedUser = "NJ",
            //        ModifiedUser = "ABA",
            //        ModifiedDate = DateTime.UtcNow,
            //        Comment = "This is the IP."
            //    },
            //    new IP
            //    {
            //        FromIP="193.168.15.26",
            //        ToIP = "193.168.15.29",
            //        CustomerId = 2,

            //        CreatedUser = "NJ",
            //        ModifiedUser = "ABA",
            //        ModifiedDate = DateTime.UtcNow,
            //        Comment = "This is the IP."
            //    },

            //    new IP
            //    {
            //        FromIP = "193.168.15.42",
            //        ToIP = "193.168.15.101",
            //        CustomerId = 2,

            //        CreatedUser = "NJ",
            //        ModifiedUser = "ABA",
            //        ModifiedDate = DateTime.UtcNow,
            //        Comment = "This is the IP."
            //    },

            //    new IP
            //    {
            //        FromIP="193.168.15.106",
            //        ToIP = "193.168.15.109",
            //        CustomerId = 2,

            //        CreatedUser = "NJ",
            //        ModifiedUser = "ABA",
            //        ModifiedDate = DateTime.UtcNow,
            //        Comment = "This is the IP."
            //    }

                
            //    );

        }
    }
}
