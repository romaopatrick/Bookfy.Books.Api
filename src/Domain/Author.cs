namespace Bookfy.Books.Api.Domain;

public class Author
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public string? Nickname { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}