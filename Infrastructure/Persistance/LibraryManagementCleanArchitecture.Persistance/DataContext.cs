// <copyright file="DataContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Persistance
{
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using LibraryManagementCleanArchitecture.Persistance.Configurations;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=.\\SQLEXPRESS;Database=LibraryDatabase;Trusted_Connection=True;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }

        public DbSet<Book> Books => this.Set<Book>();

        public DbSet<Member> Members => this.Set<Member>();

        public DbSet<MinorStaff> MinorStaff => this.Set<MinorStaff>();

        public DbSet<ManagementStaff> ManagementStaff => this.Set<ManagementStaff>();

        public DbSet<Person> People => this.Set<Person>();
    }
}
