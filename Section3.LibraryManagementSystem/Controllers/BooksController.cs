using Microsoft.AspNetCore.Mvc;
using Section3.LibraryManagementSystem.Models;

namespace Section3.LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static ICollection<Book> books = new List<Book>()
        {
            new() { Id = Guid.NewGuid(), Title = "Simyacı", Author = "Paulo Coelho", PageCount = 188 },
            new() { Id = Guid.NewGuid(), Title = "Kürk Mantolu Madonna", Author = "Sebahattin Ali", PageCount = 160 },
            new() { Id = Guid.NewGuid(), Title = "Şeker Portakalı", Author = "Jose Mauro De Vasconcelos", PageCount = 200 }
        };

        [HttpGet]
        public ActionResult<ICollection<Book>> GetAll() => Ok(books);

        [HttpGet("{bookId}")]
        public ActionResult<Book> Get(Guid bookId) =>
            books.FirstOrDefault(book =>  book.Id == bookId) is not null ? Ok(books.FirstOrDefault(book => book.Id == bookId)) : NotFound();

        [HttpPost]
        public ActionResult Add(Book book)
        { }
    }
}