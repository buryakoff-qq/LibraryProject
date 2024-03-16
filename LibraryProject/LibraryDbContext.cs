using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using System;

namespace DataAccess;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {

    }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne<Author>(book => book.Author)
            .WithMany(author => author.Books)
            .HasForeignKey(book => book.AuthorId);
    }
}
