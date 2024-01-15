using System.Text.Json.Serialization;
using Bookfy.Books.Api.Domain;

namespace Bookfy.Books.Api.src.Boundaries;

public class UpdateBook
{
    [JsonIgnore] public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Edition { get; set; }
    public IDictionary<string, string>? Settings { get; set; }
    public int? NumberOfPages { get; set; }
    public Guid? PublisherId { get; set; }
    public DateTime? PublishDate { get; set; }
    public Guid? AuthorId { get; set; }
    public List<string>? SearchTerms { get; set; } = [];
    public string? ISBNCode { get; set; }
    public string? ReferenceExpression { get; set; }

    public void FillBook(Book book)
    {
        book.Title = Title?.ToLower() ?? book.Title;
        book.Edition = Edition?.ToLower() ?? book.Edition;
        book.PublishDate = PublishDate ?? book.PublishDate;
        book.BookEasySearch.ISBNCode = ISBNCode ?? book.BookEasySearch.ISBNCode;
        book.BookEasySearch.ReferenceExpression = ReferenceExpression?.ToLower() ?? book.BookEasySearch.ReferenceExpression;
        book.BookEasySearch.SearchTerms = SearchTerms ?? book.BookEasySearch.SearchTerms;
        book.Settings = Settings ?? book.Settings;
        book.NumberOfPages = NumberOfPages ?? book.NumberOfPages;
    }
}