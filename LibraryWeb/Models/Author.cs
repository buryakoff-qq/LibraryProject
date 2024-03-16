namespace LibraryWeb.Models;

public class Author
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Surename { get; set; }
    public ICollection<Book>? Books { get; set; }
}
