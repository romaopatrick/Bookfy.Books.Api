using Microsoft.AspNetCore.Mvc;

namespace Bookfy.Books.Api.src.Boundaries
{
    public class DeleteBook
    {
        [FromRoute(Name = "id")] public Guid Id { get; init; }
    }
}