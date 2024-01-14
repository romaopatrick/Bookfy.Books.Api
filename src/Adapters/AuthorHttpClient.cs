using System.Net;
using Bookfy.Books.Api.Boundaries;
using Bookfy.Books.Api.Domain;
using Bookfy.Books.Api.Ports;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;

namespace Bookfy.Books.Api.Adapters;

public class AuthorHttpClient(
    IFlurlClientCache clientCache,
    IOptions<AuthorHttpSettings> settings): IAuthorRepository
{
    private readonly AuthorHttpSettings _settings = settings.Value;
    private readonly IFlurlClient _client = clientCache
        .GetOrAdd(nameof(AuthorHttpClient), settings.Value.BaseAddress)
        .AllowAnyHttpStatus();

    public async Task<Author?> FirstById(Guid id, CancellationToken ct)
    {
        using var response = await _client
            .Request(_settings.GetByIdPath, id)
            .GetAsync(cancellationToken: ct);

        var result = await response.GetJsonAsync<Result<Author>>();

        return result?.Data;
    }
}

public class AuthorHttpSettings
{
    public required string BaseAddress { get; init; } 
    public required string GetByIdPath { get; set; }
}