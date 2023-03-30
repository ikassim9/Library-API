using Library_Api.Models;

namespace Library_Api.Services
{
    public interface IBookService
    {

        /*
         Returns all the books in the collection
         */
        public List<Book> GetBooks();

        /*
         * Adds a book to the collection
         */
        public Book AddBook(Book book);

        /*
         * Returns a book that matches the given id
          
         */ 
        public Book Get(string id);


        /*
         * Updates the content of a book hat matches the given id
         */
        public void UpdateBook(string id, Book book);

        /*
         * Deletes a book hat matches the given id
         */
        public void DeleteBook(string id);

    }
}
