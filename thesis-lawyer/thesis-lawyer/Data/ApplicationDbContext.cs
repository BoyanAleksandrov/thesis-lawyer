using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Metadata;
using thesis_lawyer.Areas.Identity.Pages.Account;
using thesis_lawyer.Data.Migrations;
using thesis_lawyer.Entities;
using thesis_lawyer.Models;

namespace thesis_lawyer.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<History> ChatHistory { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        [ForeignKey("Id")]
        public virtual ICollection<History> History { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<History>().HasOne<UserModel>().WithMany().HasForeignKey(h => h.UserId);

            // Configure Identity related entities' primary keys
            builder.Entity<IdentityRole>().HasKey(role => role.Id);
            builder.Entity<IdentityRoleClaim<string>>().HasKey(claim => claim.Id);
            builder.Entity<IdentityUserClaim<string>>().HasKey(us => us.Id);
            builder.Entity<IdentityUserLogin<string>>().HasKey(login => new { login.LoginProvider, login.ProviderKey });
            builder.Entity<IdentityUserRole<string>>().HasKey(userRole => new { userRole.UserId, userRole.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasKey(token => new { token.UserId, token.LoginProvider, token.Name });
        }
    }
    
    
}