using CaféCozyApp.Models;
using CaféCozyApp.Models.Common;
using CaféCozyApp.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AboutFeature> AboutFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Setting>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Contact>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<ProductCategory>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Message>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Subscriber>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Service>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<AboutFeature>().HasQueryFilter(m => !m.IsDeleted);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {


            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "Admin";
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = "Admin";
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = "Admin";
                        break;

                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
