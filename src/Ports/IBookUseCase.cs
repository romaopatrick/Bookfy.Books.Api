using Bookfy.Books.Api.Boundaries;
using Bookfy.Books.Api.Domain;

namespace Bookfy.Books.Api.Ports;

public interface IBookUseCase
{
    public Task<Result<Book>> Create(CreateBook input, CancellationToken ct);
}