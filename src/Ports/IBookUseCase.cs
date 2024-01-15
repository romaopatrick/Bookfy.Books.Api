using Bookfy.Books.Api.Boundaries;
using Bookfy.Books.Api.Domain;
using Bookfy.Books.Api.src.Boundaries;

namespace Bookfy.Books.Api.Ports;

public interface IBookUseCase
{
    Task<Result<Book>> Create(CreateBook input, CancellationToken ct);
    Task<Result<Book>> Update(UpdateBook input, CancellationToken ct);
    Task<Result<Paginated<Book>>> Search(SearchBooks input, CancellationToken ct);
    Task<Result> Delete(DeleteBook input, CancellationToken ct);
}