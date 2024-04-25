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

        public DbSet<HistoryChat> HistoryChats { get; set; }
        public DbSet<Chat> Chat { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<History>().HasOne<UserModel>().WithMany().HasForeignKey(h => h.UserId);
            builder.Entity<HistoryChat>()
                .HasOne(h => h.User)
                .WithMany(u => u.HistoryChats)
                .HasForeignKey(h => h.UserForeignKey)
                .IsRequired();
            builder.Entity<UserModel>()
                .HasMany(u => u.Chats)
                .WithOne(c => c.User)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
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