namespace Bookfy.Books.Api.Domain;

public class Author
{
    public required Guid Id { get; set; }
    public required string FullName { get; set; }
}