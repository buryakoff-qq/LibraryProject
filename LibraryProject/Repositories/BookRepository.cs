using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Repositories;

public class BookRepository
{
    private readonly LibraryDbContext _context;


    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> ReadAllAsync()
    {
        var Books = await _context.Books.Include(b => b.Author).ToListAsync();
        return Books;
    }
    public async Task CreateAsync(Book Book)
    {
        _context.Books.Add(Book);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Book Book)
    {
        _context.Books.Update(Book);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(long id)
    {
        var Book = await _context.Books.FirstOrDefaultAsync(a => a.Id == id);
        _context.Books.Remove(Book);
        await _context.SaveChangesAsync();
    }
    public async Task<Book> ReadAsync(int id)
    {
        var Book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(a => a.Id == id);
        return Book;
    }
}
