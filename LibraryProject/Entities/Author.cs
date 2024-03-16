using System.Text.Json.Serialization;

namespace DataAccess.Entities;

public class Author
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Surename { get; set; }
    public ICollection<Book>? Books { get; set; }
    [JsonIgnore]
    public string Authordata
    {
        get { return $"[{Id}] {Name} {Surename}"; }
    }
}
