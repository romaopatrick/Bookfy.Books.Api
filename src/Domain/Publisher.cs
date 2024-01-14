namespace Bookfy.Books.Api.Domain;

public class Publisher 
{
    public required Guid Id { get; set; }
    public required string TradeName { get; set; }
    public required IDictionary<string, string> Settings { get; set; }
}