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
        var author = await _authorRepository.FirstById(input.AuthorId, ct);
        if(author is null)
            return Result.WithFailure<Book>("publisher_not_found", 404);

        var publisher = await _publisherRepository.FirstById(input.PublisherId, ct);
        if(publisher is null) 
            return Result.WithFailure<Book>("author_not_found", 404);

        var book = await _bookRepository.Create(new() {
            AuthorId = author.Id,
            BookEasySearch = new() {
                AuthorName = author.FullName,
                ISBNCode = input.ISBNCode,
                ReferenceExpression = input.ReferenceExpression,
                SearchTerms = input.SearchTerms,
            },
            PublishDate = input.PublishDate,
            PublisherId = publisher.Id,
            Title = input.Title,
            Edition = input.Edition,
            Settings = input.Settings
        }, ct);

        return Result.WithSuccess(book, 201);
    }
}