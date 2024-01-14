using System.Net;
using Bookfy.Books.Api.Domain;
using Bookfy.Books.Api.Ports;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace Bookfy.Books.Api.Adapters;

public class AuthorHttpClient(
    IFlurlClientCache clientCache,
    AuthorHttpSettings settings): IAuthorRepository
{
    private readonly AuthorHttpSettings _settings = settings;
    private readonly IFlurlClient _client = clientCache
        .GetOrAdd(nameof(AuthorHttpClient), settings.BaseAddress)
        .AllowAnyHttpStatus();

    public async Task<Author?> FirstById(Guid id, CancellationToken ct)
    {
        using var response = await _client
            .HttpClient
            .GetAsync(_settings.GetByIdPath, ct);

        return response.IsSuccessStatusCode 
            ? await response.Content.ReadFromJsonAsync<Author>(ct) 
            : null;
    }
}

public class AuthorHttpSettings
{
    public required string BaseAddress { get; init; } 
    public required string GetByIdPath { get; set; }
}