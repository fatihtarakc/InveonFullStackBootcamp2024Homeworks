using System.Net;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Section3.LibraryManagementSystem.Dtos.BookDtos;
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
        private readonly IMapper mapper;
        public BooksController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Books() => Ok(mapper.Map<ICollection<BookDto>>(books));

        [HttpGet("{bookId}")]
        public ActionResult<Book> Get(Guid bookId) =>
            books.FirstOrDefault(book =>  book.Id == bookId) is not null ? Ok(mapper.Map<BookDto>(books.FirstOrDefault(book => book.Id == bookId))) : NotFound("Searching book was not found !");

        [HttpPost]
        public IActionResult Add(BookAddDto bookAddDto)
        {
            var book = mapper.Map<Book>(bookAddDto);
            book.Id = Guid.NewGuid();
            books.Add(book);
            return Ok($"{book.Title} was added successfully to book list !");
        }

        [HttpDelete("{bookId}")]
        public IActionResult Delete(Guid bookId)
        {
            var book = books.FirstOrDefault(book => book.Id == bookId);
            if (book is null) return BadRequest($"This {bookId} is invalid !");

            books.Remove(book);
            return Ok($"{book.Title} was deleted successfully from book list !");
        }

        [HttpPut]
        public IActionResult Update(BookUpdateDto bookUpdateDto) 
        {
            var book = books.FirstOrDefault(book => book.Id == bookUpdateDto.Id);
            if (book is null) return BadRequest($"This {bookUpdateDto.Id} is invalid !");

            mapper.Map(bookUpdateDto, book);
            return Ok($"This book with {bookUpdateDto.Id} id number was updated successfully !");
        }
    }
}