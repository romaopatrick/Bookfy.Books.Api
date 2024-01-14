using Bookfy.Books.Api.Boundaries;
using Bookfy.Books.Api.Domain;

namespace Bookfy.Books.Api.Ports;

public interface IBookUseCase
{
    Task<Result<Book>> Create(CreateBook input, CancellationToken ct);
    Task<Result<Paginated<Book>>> Search(SearchBooks input, CancellationToken ct);
}