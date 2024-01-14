using Bookfy.Books.Api.Boundaries;
using Bookfy.Books.Api.Domain;
using Bookfy.Books.Api.Ports;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;

namespace Bookfy.Books.Api.Adapters;

public class PublisherHttpClient(
    IFlurlClientCache clientCache,
    IOptions<PublisherHttpSettings> settings) : IPublisherRepository
{
    private readonly PublisherHttpSettings _settings = settings.Value;
    private readonly IFlurlClient _client = clientCache
        .GetOrAdd(nameof(PublisherHttpClient), settings.Value.BaseAddress)
        .AllowAnyHttpStatus();

    public async Task<Publisher?> FirstById(Guid id, CancellationToken ct)
    {
        using var response = await _client
            .Request(_settings.GetByIdPath, id)
            .GetAsync(cancellationToken: ct);

        var result = await response.GetJsonAsync<Result<Publisher>>();

        return result?.Data;
    }
}
public class PublisherHttpSettings 
{
    public required string BaseAddress { get; init; }
    public required string GetByIdPath { get; set; }
} 