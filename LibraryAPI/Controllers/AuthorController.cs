using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {

        var dbcf = new LibraryDbContextFactory();
        var context = dbcf.CreateDbContext([]);
        AuthorRepository authorRepository = new AuthorRepository(context);
        List<Author> authors = await authorRepository.ReadAllAsync();
        return Ok(authors);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var dbcf = new LibraryDbContextFactory();
        var context = dbcf.CreateDbContext([]);
        AuthorRepository authorRepository = new AuthorRepository(context);
        Author author = await authorRepository.ReadAsync(id);
        return Ok(author);
    }
    [HttpPost]
    public async Task<IActionResult> CreateAuthor(Author author)
    {
        var dbcf = new LibraryDbContextFactory();
        var context = dbcf.CreateDbContext([]);
        AuthorRepository authorRepository = new AuthorRepository(context);
        await authorRepository.CreateAsync(author);
        return Ok();
    }
    [HttpPut("{id}/{name}/{surename}")]
    public async Task<IActionResult> UpdateAuthor(int id, string name, string surename)
    {
        var dbcf = new LibraryDbContextFactory();
        var context = dbcf.CreateDbContext([]);
        AuthorRepository authorRepository = new AuthorRepository(context);
        Author author = await authorRepository.ReadAsync(id);
        author.Name = name;
        author.Surename = surename;
        await authorRepository.UpdateAsync(author);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var dbcf = new LibraryDbContextFactory();
        var context = dbcf.CreateDbContext([]);
        AuthorRepository authorRepository = new AuthorRepository(context);
        await authorRepository.DeleteAsync(id);
        return Ok();
    }
}
