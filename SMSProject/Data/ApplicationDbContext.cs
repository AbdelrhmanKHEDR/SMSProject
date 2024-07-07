using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMSProject.Models;

namespace SMSProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("Users" , "security");
            builder.Entity<IdentityRole>().ToTable("Roles" , "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles" , "security");
            
        }
    }
}

