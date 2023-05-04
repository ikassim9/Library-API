using Amazon.S3;
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
        private readonly IConfiguration _config;
        private readonly IS3Service _s3;
        public BookController(IBookService bookService, IConfiguration config, IS3Service s3)
        {
            _bookService = bookService;
            _config = config;
            _s3 = s3;
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
        public async Task<ActionResult<Book>> AddBook([FromForm] Book book)
        {

            var file = Request.Form.Files[0];
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileExt = Path.GetExtension(file.FileName);
            var objName = $"{Guid.NewGuid()}.{fileExt}";
            var bucketName = _config["AwsConfiguration:BucketName"];


            var s3Obj = new S3Object()
            {
                BucketName = bucketName,
                InputStream = memoryStream,
                Name = objName
            };

            var creds = new AwsCredentials()
            {
                AwsAcessKey = _config["AwsConfiguration:AcessKey"],
                AwsSecretKey = _config["AwsConfiguration:SecretKey"],

            };

            var result = await _s3.UploadFileAsync(s3Obj, creds);


            if (result.StatusCode == 200)
            {

                book.BookCover = $"https://{bucketName}.s3.amazonaws.com/{objName}";
                book.BookCoverUrl = objName;
                _bookService.AddBook(book);

                return CreatedAtAction(nameof(Get), new { bookId = book.Id }, book);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }



        }

        [HttpPut("{bookId}")]
        public async Task<ActionResult<Book>> UpdateBook(string bookId, [FromForm] Book book)
        {
            Book existingBook = _bookService.Get(bookId);

            if (existingBook == null)
            {
                return NotFound($"Book with id {bookId} doesn' exist");
            }

            if (!string.IsNullOrEmpty(book.BookCover))
            {
                _bookService.UpdateBook(bookId, book);

            }
            else
            {
                var file = Request.Form.Files[0];
                await using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var fileExt = Path.GetExtension(file.FileName);
                var objName = $"{Guid.NewGuid()}.{fileExt}";
                var bucketName = _config["AwsConfiguration:BucketName"];

                var s3Obj = new S3Object()
                {
                    BucketName = bucketName,
                    InputStream = memoryStream,
                    Name = objName
                };

                var creds = new AwsCredentials()
                {
                    AwsAcessKey = _config["AwsConfiguration:AcessKey"],
                    AwsSecretKey = _config["AwsConfiguration:SecretKey"],

                };

                await _s3.DeleteFileAsync(existingBook.BookCoverUrl, bucketName, creds);
                var result = await _s3.UploadFileAsync(s3Obj, creds);

                if (result.StatusCode == 200)
                {

                    book.BookCover = $"https://{bucketName}.s3.amazonaws.com/{objName}";
                    book.BookCoverUrl = objName;
                    _bookService.UpdateBook(bookId, book);
                }

            }
            return NoContent();
        }

   

    [HttpDelete("{bookId}")]
        public async Task<ActionResult<Book>> DeleteBook(string bookId)
        {
            Book existingBook = _bookService.Get(bookId);

            if (existingBook == null)
            {
                return NotFound($"Book with id {bookId} does not exist");
            }

            var creds = new AwsCredentials()
            {
                AwsAcessKey = _config["AwsConfiguration:AcessKey"],
                AwsSecretKey = _config["AwsConfiguration:SecretKey"],

            };

            var bucketName = _config["AwsConfiguration:BucketName"];


            var result = await _s3.DeleteFileAsync(existingBook.BookCoverUrl, bucketName, creds);

            if (result.StatusCode == 200)
            {
                _bookService.DeleteBook(bookId);
                return NoContent();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);




        }
    }
}