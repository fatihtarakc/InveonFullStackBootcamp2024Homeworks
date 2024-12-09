using Microsoft.AspNetCore.Mvc;
using Section3.LibraryManagementSystem.Models;

namespace Section3.Pagination.Controllers
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

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public IActionResult Books(int pageNumber, int pageSize)
        {
            var pagedBooks = books.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var totalBooks = books.Count;
            var totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);

            var response = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalBooks = totalBooks,
                TotalPages = totalPages,
                Books = pagedBooks
            };

            return Ok(response);
        }
    }
}