using System.Linq.Expressions;
using Bookfy.Books.Api.Boundaries;
using Bookfy.Books.Api.Domain;
using Bookfy.Books.Api.Ports;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bookfy.Books.Api.Adapters
{
    public class BookMongoDb(IMongoClient mongoClient, IOptions<MongoDbSettings> dbSettings) : IBookRepository
    {
        private readonly IMongoCollection<Book> _collection =
            mongoClient
                .GetDatabase(dbSettings.Value.Database)
                .GetCollection<Book>(nameof(Book));

        public async Task<Book> Create(Book book, CancellationToken ct)
        {
            book.Id = Guid.NewGuid();
            book.CreatedAt = DateTime.UtcNow;
            await _collection
                .InsertOneAsync(book, 
                    cancellationToken: ct);

            return book;
        }

        public async Task Delete(Guid id, CancellationToken ct) 
            => await _collection.DeleteOneAsync(x => x.Id == id, ct);

        public Task<Book> First(Expression<Func<Book, bool>> predicate, CancellationToken ct)
            => _collection.Find(predicate).FirstOrDefaultAsync(ct);

        public async Task<Paginated<Book>> Get(Expression<Func<Book, bool>> filter, long skip, long take, CancellationToken ct)
        {
            var filtered = _collection.Find(filter);
            var total = await filtered.CountDocumentsAsync(ct); 
            var results = await filtered.Skip((int)skip)
                .Limit((int)take)
                .SortBy(x => x.Title)
                .ToListAsync(cancellationToken: ct);
                
            return new Paginated<Book> 
            {
                Total = total,
                Results = results,
            };
        }
        

        public async Task<Book> Update(Book book, CancellationToken ct)
        {
            book.UpdatedAt = DateTime.UtcNow;
            await _collection
                .ReplaceOneAsync(
                    x => x.Id == book.Id,
                    book,
                    cancellationToken: ct);

            return book;
        }

    }
}