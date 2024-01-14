using Bookfy.Books.Api.Boundaries;
using Bookfy.Books.Api.Domain;
using Bookfy.Books.Api.Ports;

namespace Bookfy.Books.Api.Adapters;

public class BookService(
    IBookRepository bookRepository,
    IPublisherRepository publisherRepository,
    IAuthorRepository authorRepository) : IBookUseCase
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IPublisherRepository _publisherRepository = publisherRepository;

    public async Task<Result<Book>> Create(CreateBook input, CancellationToken ct)
    {
        if (await BookConflicts(input.Title, input.Edition, input.PublisherId, input.AuthorId, ct))
            return Result.WithFailure<Book>("book_conflicts", 409);

        var author = await _authorRepository.FirstById(input.AuthorId, ct);
        if (author is null)
            return Result.WithFailure<Book>("author_not_found", 404);

        var publisher = await _publisherRepository.FirstById(input.PublisherId, ct);
        if (publisher is null)
            return Result.WithFailure<Book>("publish_not_found", 404);

        var book = await _bookRepository.Create(new()
        {
            Author = author,
            Publisher = publisher,
            BookEasySearch = new()
            {
                ISBNCode = input.ISBNCode,
                ReferenceExpression = input.ReferenceExpression?.ToLower(),
                SearchTerms = input.SearchTerms,
            },
            PublishDate = input.PublishDate,
            Title = input.Title.ToLower(),
            Edition = input.Edition?.ToLower(),
            Settings = input.Settings
        }, ct);

        return Result.WithSuccess(book, 201);
    }

    public async Task<Result<Paginated<Book>>> Search(SearchBooks input, CancellationToken ct)
    {
        var result = await _bookRepository.Get(x
            => x.Title.Contains(input.SearchTerm ?? "", StringComparison.CurrentCultureIgnoreCase)
            || x.Edition!.Contains(input.SearchTerm ?? "", StringComparison.CurrentCultureIgnoreCase)
            || x.Author.FullName.Contains(input.SearchTerm ?? "", StringComparison.CurrentCultureIgnoreCase)
            || x.Publisher.TradeName.Contains(input.SearchTerm ?? "", StringComparison.CurrentCultureIgnoreCase)
            || x.BookEasySearch.ReferenceExpression!.Contains(input.SearchTerm ?? "", StringComparison.CurrentCultureIgnoreCase)
            || x.BookEasySearch.SearchTerms.Any(x => x.Contains(input.SearchTerm ?? "", StringComparison.CurrentCultureIgnoreCase))
        , input.Skip ?? 0, input.Take ?? 10, ct);


        return Result.WithSuccess(result, 200);
    }

    private async Task<bool> BookConflicts(
        string title, string? edition,
        Guid publisherId, Guid authorId, CancellationToken ct)
    {
        var count = await _bookRepository.Count(x =>
                x.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                x.Edition!.Equals(edition, StringComparison.OrdinalIgnoreCase) &&
                x.Publisher.Id == publisherId &&
                x.Author.Id == authorId
            , ct);

        return count > 0;
    }
}