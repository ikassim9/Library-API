using Library_Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Library_Api.Services
{
    public class BookService : IBookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(ILibraryDatabaseSettings settings, IMongoClient mongoClient)
        {

            var mongoDb = mongoClient.GetDatabase(settings.DatabaseName);

            _books = mongoDb.GetCollection<Book>(settings.LibraryCollectionName);

        }

        public List<Book> GetBooks()
        {

            return _books.Find(book => true).ToList();



        }
    }
}
