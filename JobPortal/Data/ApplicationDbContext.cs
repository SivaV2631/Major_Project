using JobPortal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Data
{
    public class ApplicationDbContext
        :IdentityDbContext   //DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ApplyJob> ApplyJobs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<JobNature> JobNatures { get; set; }
        public DbSet<JobRequirement> JobRequirements { get; set; }
        public DbSet<PostJob> PostJobs { get; set; }
        public DbSet<UserTable> UserTables { get; set; }
        public DbSet<UserType> UserTypes { get; set; }




    }
}
