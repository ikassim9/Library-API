using Library_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
  
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
         
        public BookController(IBookService bookService )
        {
            _bookService = bookService;

        }

        [HttpGet]
        public IActionResult GetBooks()
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
    }
}
