using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public AppDbContext() : base(GetConnectionString())
    {
        try
        {
            Database.Log = s => Debug.WriteLine(s);
            Database.SetInitializer<AppDbContext>(null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing database context: {ex}");
        }
    }

    private static string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["DBEmployees"].ConnectionString;
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        modelBuilder.Entity<Employee>().Ignore(e => e.RowVersion);
    }
}