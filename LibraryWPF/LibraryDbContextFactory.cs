using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LibraryWpf;

public class AppDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
{
    public LibraryDbContext CreateDbContext(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder().
            SetBasePath(Directory.
            GetCurrentDirectory()).
            AddJsonFile("appsettings.json").
            Build();
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 32)));
        return new LibraryDbContext(optionsBuilder.Options);
    }
}


