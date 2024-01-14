namespace Bookfy.Books.Api.Domain;

public class Book
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Edition { get; set; }
    public IDictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
    public required Guid PublisherId { get; set; }
    public required DateOnly PublishDate { get; set; }
    public required Guid AuthorId { get; set; }
    public required BookEasySearch BookEasySearch { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}