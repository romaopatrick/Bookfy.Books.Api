namespace Bookfy.Books.Api.Boundaries;

public class CreateBook 
{
    public required string Title { get; set; }
    public string? Edition { get; set; }
    public IDictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
    public required Guid PublisherId { get; set; }
    public required DateOnly PublishDate { get; set; }
    public required Guid AuthorId { get; set; }
    public List<string> SearchTerms { get; set; } = [];
    public string? ISBNCode { get; set; }
    public string? ReferenceExpression { get; set; }
}