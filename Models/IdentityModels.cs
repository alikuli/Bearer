using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.People;

namespace Bearer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        [Display(Name="Activate User?")]
        public bool Active { get; set; }


        [Column(TypeName = "DateTime2")]
        [Display(Name = "Created (UTC)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }


        [Display(Name = "Created By ")]
        [MaxLength(50)]
        public string CreatedUser { get; set; }


        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified (UTC)")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Modified By")]
        [MaxLength(50)]
        public string ModifiedUser { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }


        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Login (UTC)")]
        public DateTime? LastLogin { get; set; }

        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Lockout (UTC)")]
        public DateTime? LastLockout { get; set; }


        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Signin Fail (UTC)")]
        public DateTime? LastSignInFailure{ get; set; }

        [Display(Name = "# of Logins")]
        [DisplayFormat(DataFormatString = "{0:#,0}", ApplyFormatInEditMode = false)]
        public int NoOfLogins { get; set; }

        public string IpAddressOfLastLogin { get; set; }
        

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }


    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<SetUp> SetUps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SetUp>()
                .Property(u => u.Description)
                .HasMaxLength(100);

            modelBuilder.Entity<SetUp>()
                .Property(u => u.Value)
                .HasMaxLength(100);

            modelBuilder.Entity<SetUp>()
                .Property(u => u.Name)
                .HasMaxLength(100);
             
            //got this from
            //This creates a middle table for a many to many relarionship
            //http://stackoverflow.com/questions/19342908/how-to-create-a-many-to-many-mapping-in-entity-framework


            modelBuilder.Entity<PersonLanguage>()
                //.HasKey(c => new { c.PersonId, c.LanguageId });
                .HasKey(c => c.Id);

            modelBuilder.Entity<Person>()
                .HasMany(c => c.PersonLanguage)
                .WithRequired()
                .HasForeignKey(c => c.PersonId);

            modelBuilder.Entity<Language>()
                .HasMany(c => c.PersonLanguage)
                .WithRequired()
                .HasForeignKey(c => c.LanguageId);


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        }

        public System.Data.Entity.DbSet<ModelsClassLibrary.Models.People.Person> People { get; set; }

        public System.Data.Entity.DbSet<ModelsClassLibrary.Models.People.Language> Languages { get; set; }

        public System.Data.Entity.DbSet<ModelsClassLibrary.Models.People.PersonLanguage> PersonLanguages { get; set; }

        

    }
}