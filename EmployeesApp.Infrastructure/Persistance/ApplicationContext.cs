using EmployeesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeesApp.Infrastructure.Persistance;

public class ApplicationContext(DbContextOptions<ApplicationContext> options, ILogger <ApplicationContext> logger)
    : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Salary)
            .HasColumnType(SqlDbType.Money.ToString())
            .HasDefaultValue(0m)
            .IsRequired();

        modelBuilder.Entity<Employee>()
            .Property(e => e.Bonus)
            .HasColumnType(SqlDbType.Money.ToString())
            .HasDefaultValue(0m)
            .IsRequired();

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Employees)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Employee>().HasData(
            new Employee()
            {
                Id = 562,
                Name = "Anders Hejlsberg",
                Email = "Anders.Hejlsberg@outlook.com",
            },
            new Employee()
            {
                Id = 62,
                Name = "Kathleen Dollard",
                Email = "k.d@outlook.com",
            },
            new Employee()
            {
                Id = 15662,
                Name = "Mads Torgersen",
                Email = "Admin.Torgersen@outlook.com",
            });


    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in modifiedEntries)
        {
            var entityName = entry.Entity.GetType().Name;
            var primaryKey = entry.Properties
                .FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue;

            foreach (var prop in entry.Properties)
            {
                if (prop.IsModified)
                {
                    var original = prop.OriginalValue?.ToString() ?? "null";
                    var current = prop.CurrentValue?.ToString() ?? "null";

                    logger.LogInformation(
                        $"{entityName} ({primaryKey}), {prop.Metadata.Name}: {original} -> {current}");
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}