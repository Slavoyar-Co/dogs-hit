﻿using Domain.Entities;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<User> Users { get; init; } = null!;

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMapping());
        }

    }
}
