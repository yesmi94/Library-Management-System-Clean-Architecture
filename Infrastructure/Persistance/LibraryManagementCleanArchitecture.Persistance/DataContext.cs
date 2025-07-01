
namespace LibraryManagementCleanArchitecture.Persistance
{
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using LibraryManagementCleanArchitecture.Persistance.Configurations;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
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

        public DbSet<Book> Books => Set<Book>();

        public DbSet<Member> Members => Set<Member>();

        public DbSet<MinorStaff> MinorStaff => Set<MinorStaff>();

        public DbSet<ManagementStaff> ManagementStaff => Set<ManagementStaff>();

        public DbSet<Person> People => Set<Person>();
        


    }
}
