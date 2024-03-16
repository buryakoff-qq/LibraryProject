using LibraryWeb.Models;
using System.Net.Http.Json;

namespace LibraryWeb.Services;

public class AuthorsService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7218/api/author";

    public AuthorsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Author>> GetAllAuthorsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error: Cannot retrieve authors");
            }
            else
            {
                return await response.Content.ReadFromJsonAsync<List<Author>>(); ;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error: " + ex.Message);
        }
    }
    public async Task<Author> GetAuthorByIdAsync(long id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Author>();
        }
        else
        {
            throw new Exception($"Error: Cannot retrieve author with ID: {id}");
        }
    }
    public async Task CreateAuthorAsync(Author author)
    {
        var response = await _httpClient.PostAsJsonAsync(_baseUrl, author);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: Cannot create author");
        }

    }
    public async Task UpdateAuthorAsync(Author author)
    {
        var response = await _httpClient.PutAsync($"{_baseUrl}/{author.Id}/{author.Name}/{author.Surename}", null);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: Cannot update author with ID: {author.Id}");
        }
    }
    public async Task DeleteAuthorAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: Cannot delete author with ID: {id}");
        }
    }
}

