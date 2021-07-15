using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecondHandClothes.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecondHandClothes.Data
{
    public class SecondHandDbContext : IdentityDbContext
    {
        public SecondHandDbContext(DbContextOptions<SecondHandDbContext> options)
            : base(options)
        {}

        public DbSet<Sex> Sexes { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<ProductImage> ProductImages { get; init; }

        public DbSet<Product> Products { get; init; }

        public DbSet<Condition> Conditions { get; init; }

        public DbSet<Comment> Comments { get; init; }


    }
}
