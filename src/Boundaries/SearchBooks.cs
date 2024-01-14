using Microsoft.AspNetCore.Mvc;

namespace Bookfy.Books.Api.Boundaries;

public class SearchBooks
{
    
    [FromQuery(Name = "searchTerm")] public string? SearchTerm { get; init; }
    [FromQuery(Name = "skip")] public long? Skip { get; init; }
    [FromQuery(Name = "take")] public long? Take { get; init; }
}