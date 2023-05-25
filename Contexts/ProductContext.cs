using Microsoft.EntityFrameworkCore;
using WebApp.Models.Entities;

namespace WebApp.Contexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public DbSet<ProductEntity> ProductItems { get; set; }
        public DbSet<PriceEntity> ProductPrices { get; set; }
        public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<ProductTagEntity> ProductTags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceEntity>()
                .Property(p => p.OriginalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PriceEntity>()
                .Property(p => p.DiscountPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PriceEntity>()
                .Property(p => p.OrdinaryPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductEntity>()
                .HasOne(p => p.Category)
                .WithOne(c => c.Product)
                .HasForeignKey<ProductCategoryEntity>(c => c.ProductId);

            modelBuilder.Entity<TagEntity>().HasData(
                new TagEntity { Id = 1, TagName = "New" },
                new TagEntity { Id = 2, TagName = "Featured" },
                new TagEntity { Id = 3, TagName = "Popular" }
        );

            base.OnModelCreating(modelBuilder);
        }

    }
}