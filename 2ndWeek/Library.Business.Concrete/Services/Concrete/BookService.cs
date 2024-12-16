namespace Library.Business.Concrete.Services.Concrete
{
    public class BookService : IBookService 
    {
        private readonly ICacheService<Book> cacheService;
        private readonly IBookRepository bookRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<BookService> logger;
        public BookService(ICacheService<Book> cacheService, IBookRepository bookRepository, IUnitOfWork unitOfWork, ILogger<BookService> logger)
        {
            this.cacheService = cacheService;
            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
    }
}