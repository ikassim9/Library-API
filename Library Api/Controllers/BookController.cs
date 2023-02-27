using Library_Api.Models;
using Library_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;

        }

        [HttpGet]
        public ActionResult<Book[]> GetBooks()
        {
            try
            {
                return Ok(_bookService.GetBooks());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpGet("{bookId}")]
        public ActionResult<Book> Get(string bookId)
        {
            Book existingBook = _bookService.Get(bookId);

            if (existingBook == null)
            {
                return NotFound($"Book with id {bookId} doesn't exist");
            }
            return existingBook;
        }

        [HttpPost]
        public ActionResult<Book> AddBook([FromBody] Book book)
        {

            _bookService.AddBook(book);

            return CreatedAtAction(nameof(Get), new { bookId = book.Id }, book);
        }

        [HttpPut("{bookId}")]
        public ActionResult<Book> UpdateBook(string bookId, [FromBody] Book book)
        {
            Book existingBook = _bookService.Get(bookId);
            
            if(existingBook == null)
            {
                return NotFound($"Book with id {bookId} doesn' exist");
            }

            _bookService.UpdateBook(bookId, book);

            return NoContent();


        }

        [HttpDelete("{bookId}")]
        public ActionResult<Book> DeleteBook(string bookId)
        {
            Book existingBook = _bookService.Get(bookId);

            if (existingBook == null)
            {
                return NotFound($"Book with id {bookId} does not exist");
            }

            _bookService.DeleteBook(bookId);

            return NoContent();
        }
             
    }
}