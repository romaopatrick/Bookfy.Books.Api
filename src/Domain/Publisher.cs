namespace Bookfy.Books.Api.Domain;

public class Publisher 
{
    public Guid Id { get; set; }
    public required string TradeName { get; set; }
    public required IDictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
}