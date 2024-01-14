using System.Net;
using Bookfy.Books.Api.Boundaries;
using Bookfy.Books.Api.Ports;
using Bookfy.Books.Api.src.Boundaries;
using Microsoft.AspNetCore.Mvc;

namespace Bookfy.Books.Api.src.Adapters;

public static class BookRoutes
{
    public static RouteGroupBuilder MapBookRoutes(this RouteGroupBuilder app)
    {
        var book = app.MapGroup("books");

        book.MapGet("search", 
            async (CancellationToken ct,
            IBookUseCase useCase,
            [AsParameters] SearchBooks input) => 
            {
                var result = await useCase.Search(input, ct);

                return JsonFromResult(result);
            }
        );

        book.MapPost("",
            async (IBookUseCase useCase,
            CancellationToken ct,
            [FromBody] CreateBook input) =>
            {
                var result = await useCase.Create(input, ct);
                return JsonFromResult(result);
            });

        return app;
    }

    private static IResult JsonFromResult<T>(Result<T> result) =>
            result.Code == (int)HttpStatusCode.NoContent
                ? Results.NoContent()
                : Results.Json(
                    data: result,
                    statusCode: result.Code);
}