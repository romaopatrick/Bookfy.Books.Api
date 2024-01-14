using Bookfy.Books.Api.Domain;
using Bookfy.Books.Api.Ports;

namespace Bookfy.Books.Api.src.Adapters;

public class PublisherHttpClient : IPublisherRepository
{
    public Task<Publisher> FirstById(Guid id, CancellationToken ct)
    {
        return Task.FromResult(new Publisher
        {
            Id = id,
            Settings = default!,
            TradeName = default!,
        });
    }
}