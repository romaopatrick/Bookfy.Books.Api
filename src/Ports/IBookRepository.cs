using System.Linq.Expressions;
using Bookfy.Books.Api.Boundaries;
using Bookfy.Books.Api.Domain;

namespace Bookfy.Books.Api.Ports;

public interface IBookRepository
{
    Task<Book> First(Expression<Func<Book, bool>> predicate, CancellationToken ct);
    Task<Paginated<Book>> Get(Expression<Func<Book, bool>> filter, long skip, long take, CancellationToken ct);
    Task<Book> Create(Book book, CancellationToken ct);
    Task<Book> Update(Book book, CancellationToken ct);
    Task Delete(Guid id, CancellationToken ct);
}