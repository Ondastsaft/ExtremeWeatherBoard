using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserData>()
                   .HasMany(u => u.ReceivedMessages)
                   .WithOne(m => m.Receiver)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<UserData>()
                .HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.Categories)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<SubCategory>()
                .HasOne(s => s.Creator)
                .WithMany(u => u.SubCategories)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<SubCategory>()
                .HasOne(s => s.ParentCategory)
                .WithMany(u => u.SubCategories)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AdminUserData>()
                   .HasMany(a => a.SubCategories)
                   .WithOne(s => s.Creator)
                   .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserData>()
                    .HasMany(a => a.Threads)
                    .WithOne(s => s.CreatorUser)
                    .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<ExtremeWeatherBoard.Models.UserData> UserDatas { get; set; } = default!;
        public DbSet<ExtremeWeatherBoard.Models.AdminUserData> AdminUserDatas { get; set; } = default!;
        public DbSet<ExtremeWeatherBoard.Models.AdminLog> AdminLogs { get; set; } = default!;
        public DbSet<ExtremeWeatherBoard.Models.Message> Messages { get; set; } = default!;
        public DbSet<ExtremeWeatherBoard.Models.Category> Categories { get; set; } = default!;
        public DbSet<ExtremeWeatherBoard.Models.SubCategory> SubCategories { get; set; } = default!;
        public DbSet<ExtremeWeatherBoard.Models.Comment> Comments { get; set; } = default!;
        public DbSet<ExtremeWeatherBoard.Models.Thread> Threads { get; set; } = default!;
    }
}
