using Microsoft.EntityFrameworkCore;
using SecondHandClothes.Services.Products.Models;
namespace SecondHandClothes.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SecondHandClothes.Data.Models;

    public class SecondHandDbContext : IdentityDbContext<User>
    {
        public SecondHandDbContext(DbContextOptions<SecondHandDbContext> options)
            : base(options)
        {}

        public DbSet<Seller> Sellers { get; init; }

        public DbSet<Sex> Sexes { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<ProductImage> ProductImages { get; init; }

        public DbSet<Product> Products { get; init; }

        public DbSet<Condition> Conditions { get; init; }

        public DbSet<Comment> Comments { get; init; }

        public DbSet<Size> Sizes { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
                .Entity<Product>()
                .HasOne(c => c.Seller)
                .WithMany(s => s.Products)
                .HasForeignKey(c => c.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(c => c.Sex)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.SexId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(c => c.Condition)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.ConditionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(c => c.Size)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.SizeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Seller>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Seller>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }

        public DbSet<SecondHandClothes.Services.Products.Models.ProductDetailsServiceModel> ProductDetailsServiceModel { get; set; }


    }
}
