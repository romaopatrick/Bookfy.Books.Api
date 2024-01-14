namespace Bookfy.Books.Api.Domain;

public class BookEasySearch
{
    public required string AuthorName { get; set; }
    public List<string> SearchTerms { get; set; } = [];
    public string? ISBNCode { get; set; }
    public string? ReferenceExpression { get; set; }
}