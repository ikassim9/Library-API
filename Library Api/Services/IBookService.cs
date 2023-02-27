using Library_Api.Models;

namespace Library_Api.Services
{
    public interface IBookService
    {

        public List<Book> GetBooks();

        public Book AddBook(Book book);

        public Book Get(string id);

        public void UpdateBook(string id, Book book);

        public void DeleteBook(string id);

    }
}
