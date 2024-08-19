using DotNet.ServiceTemplate.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNet.ServiceTemplate.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
                    .Property(p => p.Id)
                    .ValueGeneratedOnAdd();
    }

    public DbSet<Person> Persons { get; set; }
}
