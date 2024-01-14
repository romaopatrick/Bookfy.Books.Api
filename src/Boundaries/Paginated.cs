namespace Bookfy.Books.Api.Boundaries;

public class Paginated<T>
{
    public List<T>? Results { get; set; }
    public long Total { get; set; }
}