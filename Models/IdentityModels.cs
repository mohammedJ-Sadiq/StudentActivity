using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentActivity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student_Program> StudentPrograms { get; set; }

        public DbSet<Student_Club> StudentClubs { get; set; }

        public DbSet<Admin_Program> AdminPrograms { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student_Program>().ToTable("Student_Program");
            modelBuilder.Entity<Student_Program>()
                .HasKey(sp => new { sp.StudentId, sp.ProgramId });

            modelBuilder.Entity<Program>()
                .HasRequired<Club>(p => p.Club)
                .WithMany(c => c.Programs)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student_Club>().ToTable("Student_Club");
            modelBuilder.Entity<Student_Club>()
                .HasKey(sc => new { sc.StudentId, sc.ClubId });

            modelBuilder.Entity<Admin_Program>().ToTable("Admin_Program");
            modelBuilder.Entity<Admin_Program>()
                .HasKey(ap => new { ap.AdminId, ap.ProgramId });




        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}