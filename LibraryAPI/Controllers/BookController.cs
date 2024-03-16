using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace LibraryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var dbcf = new LibraryDbContextFactory();
        var context = dbcf.CreateDbContext([]);
        BookRepository bookRepository = new BookRepository(context);
        List<Book> books = await bookRepository.ReadAllAsync();
        return Ok(books);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var dbcf = new LibraryDbContextFactory();
        var context = dbcf.CreateDbContext([]);
        BookRepository bookRepository = new BookRepository(context);
        Book book = await bookRepository.ReadAsync(id);
        return Ok(book);
    }
    [HttpPost]
    public async Task<IActionResult> CreateBook(Book book)
    {
        var dbcf = new LibraryDbContextFactory();
        var context = dbcf.CreateDbContext([]);
        BookRepository bookRepository = new BookRepository(context);
        await bookRepository.CreateAsync(book);
        return Ok();
    }
    [HttpPut("{id}/{title}/{genre}")]
    public async Task<IActionResult> UpdateBook(int id, string title, string genre)
    {
        var dbcf = new LibraryDbContextFactory();
        var context = dbcf.CreateDbContext([]);
        BookRepository bookRepository = new BookRepository(context);
        Book book = await bookRepository.ReadAsync(id);
        book.Title = title;
        book.Genre = genre;
        await bookRepository.UpdateAsync(book);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var dbcf = new LibraryDbContextFactory();
        var context = dbcf.CreateDbContext([]);
        BookRepository bookRepository = new BookRepository(context);
        await bookRepository.DeleteAsync(id);
        return Ok();
    }
}
