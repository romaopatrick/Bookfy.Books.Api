namespace Bookfy.Books.Api.Domain;

public class BookEasySearch
{
    public List<string> SearchTerms { get; set; } = [];
    public string? ISBNCode { get; set; }
    public string? ReferenceExpression { get; set; }
}