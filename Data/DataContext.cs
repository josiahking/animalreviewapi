using AnimalReviewApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<AnimalOwner> AnimalOwners { get; set; }
        public DbSet<AnimalCategory> AnimalCategories { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalCategory>()
                .HasKey(ac => new {ac.AnimalId, ac.CategoryId});
            modelBuilder.Entity<AnimalCategory>()
                .HasOne(a => a.Animal)
                .WithMany(ac => ac.AnimalCategories)
                .HasForeignKey(a => a.AnimalId);
            modelBuilder.Entity<AnimalCategory>()
                .HasOne(a => a.Category)
                .WithMany(ac => ac.AnimalCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<AnimalOwner>()
                .HasKey(ao => new {ao.AnimalId, ao.OwnerId });
            modelBuilder.Entity<AnimalOwner>()
                .HasOne(a => a.Animal)
                .WithMany(ao => ao.AnimalOwners)
                .HasForeignKey(a => a.AnimalId);
            modelBuilder.Entity<AnimalOwner>()
                .HasOne(a => a.Owner)
                .WithMany(ao => ao.AnimalOwners)
                .HasForeignKey(o => o.OwnerId);
        }
    }
}
