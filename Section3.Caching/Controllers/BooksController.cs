using Microsoft.AspNetCore.Mvc;
using Section3.Caching.Services.RedisCacheServices;
using Section3.LibraryManagementSystem.Models;

namespace Section3.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> logger;
        private readonly IRedisCacheService redisCacheService;
        public BooksController(ILogger<BooksController> logger, IRedisCacheService redisCacheService)
        {
            this.logger = logger;
            this.redisCacheService = redisCacheService;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Books()
        {
            const string cacheKey = "BookList";
            var cachedData = await redisCacheService.GetCacheAsync<IEnumerable<Book>>(cacheKey); 
            if (cachedData != null) 
            { 
                logger.LogInformation("Data fetched from cache."); 
                return cachedData; 
            }

            var data = new List<Book>()
            { 
                new Book { Id = Guid.NewGuid(), Title = "Sample Book 1", Author = "Author 1" }, 
                new Book { Id = Guid.NewGuid(), Title = "Sample Book 2", Author = "Author 2" } 
            };

            await redisCacheService.SetCacheAsync(cacheKey, data, TimeSpan.FromMinutes(5)); 
            logger.LogInformation("Data cached.");

            return data;
        }
    }
}