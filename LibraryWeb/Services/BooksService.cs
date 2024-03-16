using LibraryWeb.Models;
using System.Net.Http.Json;

namespace LibraryWeb.Services;

public class BooksService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7218/api/book";

    public BooksService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        var response = await _httpClient.GetAsync(_baseUrl);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error: Cannot retrieve books");
        }
        else
        {
            return await response.Content.ReadFromJsonAsync<List<Book>>(); ;
        }
    }
    public async Task<Book> GetBookByIdAsync(long id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Book>();
        }
        else
        {
            throw new Exception($"Error: Cannot retrieve book with ID: {id}");
        }
    }
    public async Task CreateBookAsync(Book book)
    {
        var response = await _httpClient.PostAsJsonAsync(_baseUrl, book);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: Cannot create book");
        }

    }
    public async Task UpdateBookAsync(Book book)
    {
        var response = await _httpClient.PutAsync($"{_baseUrl}/{book.Id}/{book.Title}/{book.Genre}", null);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: Cannot update book");
        }
    }
    public async Task DeleteBookAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: Cannot delete book with ID: {id}");
        }
    }
}
