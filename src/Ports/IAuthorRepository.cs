using Bookfy.Books.Api.Domain;
using Flurl.Http;

namespace Bookfy.Books.Api.Ports;

public interface IAuthorRepository
{
    public Task<Author?> FirstById(Guid id, CancellationToken ct);
}