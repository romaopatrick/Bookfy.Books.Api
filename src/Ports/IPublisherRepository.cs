using Bookfy.Books.Api.Domain;

namespace Bookfy.Books.Api.Ports;


public interface IPublisherRepository
{
    public Task<Publisher?> FirstById(Guid id, CancellationToken ct);
}