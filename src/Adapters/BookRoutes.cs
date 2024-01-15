using System.Net;
using Bookfy.Books.Api.Boundaries;
using Bookfy.Books.Api.Ports;
using Bookfy.Books.Api.src.Boundaries;
using Microsoft.AspNetCore.Mvc;

namespace Bookfy.Books.Api.Adapters;

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
        book.MapPut("{id:guid}",
            async (IBookUseCase useCase,
            CancellationToken ct,
            [FromRoute] Guid id,
            [FromBody] UpdateBook input) =>
            {
                input.Id = id;
                var result = await useCase.Update(input, ct);
                return JsonFromResult(result);
            });

        book.MapDelete("{id:guid}",
             async (IBookUseCase useCase,
            CancellationToken ct,
            [AsParameters] DeleteBook input) =>
            {
                var result = await useCase.Delete(input, ct); 
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

    private static IResult JsonFromResult(Result result) =>
            result.Code == (int)HttpStatusCode.NoContent
                ? Results.NoContent()
                : Results.Json(
                    data: result,
                    statusCode: result.Code);
}