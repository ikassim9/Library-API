using Library_Api.Models;

namespace Library_Api.Services
{
    public interface IBookService
    {

        public List<Book> GetBooks();
    }
}
