using Library_Api.Models;
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

        public Book Get(string id)
        {
            return _books.Find(book => book.Id == id).FirstOrDefault();
        }


        public Book AddBook(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void UpdateBook(string bookId, Book book)
        {
            _books.ReplaceOne(book => book.Id == bookId, book);
        }

        public void DeleteBook(string id)
        {
            _books.DeleteOne(book => book.Id == id);
        }
    }
}