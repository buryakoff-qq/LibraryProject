using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AuthorRepository
{
    private readonly LibraryDbContext _context;


    public AuthorRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Author>> ReadAllAsync()
    {
        var authors = await _context.Authors
            .Include(a => a.Books)
            .ToListAsync();
        return authors;
    }
    public async Task CreateAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(long id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }
    public async Task<Author> ReadAsync(int id)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);
        return author;
    }
}
