using Library_Api.model;

namespace Library_Api.Services
{
    public class BookService : IBookService
    {


        public List<Book> GetBooks()
        {

            return new List<Book>()
            {

                new Book
                {
                    Title = "Test",
                    Price = 20.40,


                }
            };

        }
    }
}
