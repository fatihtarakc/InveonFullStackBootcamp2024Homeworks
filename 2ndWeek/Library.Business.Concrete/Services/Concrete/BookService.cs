using System;

namespace Library.Business.Concrete.Services.Concrete
{
    public class BookService : IBookService
    {
        private readonly IAccountService accountService;
        private readonly ICacheService<Book> cacheService;
        private readonly IAppUserRepository appUserRepository;
        private readonly IBookRepository bookRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IStringLocalizer<MessageResources> stringLocalizer;
        private readonly ILogger<BookService> logger;
        public BookService(IAccountService accountService, ICacheService<Book> cacheService, IAppUserRepository appUserRepository, IBookRepository bookRepository, IUnitOfWork unitOfWork, IStringLocalizer<MessageResources> stringLocalizer, ILogger<BookService> logger)
        {
            this.accountService = accountService;
            this.cacheService = cacheService;
            this.appUserRepository = appUserRepository;
            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
            this.stringLocalizer = stringLocalizer;
            this.logger = logger;
        }

        public async Task<IDataResult<BookDto>> AddAsync(BookAddDto bookAddDto, string appUserUsername)
        {
            try
            {
                var identityUser = await accountService.FindByNameAsync(appUserUsername);
                if (identityUser is null) return new ErrorDataResult<BookDto>(stringLocalizer[Messages.Account_Was_Not_Found]);

                var appUser = await appUserRepository.GetFirstOrDefaultAsync(appUser => appUser.IdentityId == identityUser.Id);
                if (appUser is null) return new ErrorDataResult<BookDto>(stringLocalizer[Messages.Book_CouldNotBe_Added]);

                var book = bookAddDto.Adapt<Book>();
                book.AppUsers.Add(appUser);

                await bookRepository.AddAsync(book);
                await unitOfWork.SaveChangesAsync();

                book = await bookRepository.GetFirstOrDefaultAsync(book => book.AppUsers.FirstOrDefault(dbAppUser => dbAppUser.Id == appUser.Id) == appUser);

                await cacheService.SetCacheAsync($"Book_{book.Id}", book, TimeSpan.FromDays(30));

                return new SuccessDataResult<BookDto>(book.Adapt<BookDto>(), stringLocalizer[Messages.Book_HasBeen_Added]);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return new ErrorDataResult<BookDto>($"{stringLocalizer[Messages.Book_CouldNotBe_Added]} : {exception.Message}");
            }
        }

        public async Task<IResult> DeleteAsync(Guid bookId)
        {
            try
            {
                var book = await bookRepository.GetByIdAsync(bookId);
                if (book is null) return new ErrorResult(stringLocalizer[Messages.Book_Was_Not_Found]);

                await bookRepository.DeleteAsync(book);
                await unitOfWork.SaveChangesAsync();

                await cacheService.SetCacheAsync($"Book_{bookId}", null, TimeSpan.Zero);
                return new SuccessResult($"{stringLocalizer[Messages.Book_HasBeen_Deleted]}");
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return new ErrorResult($"{stringLocalizer[Messages.Book_CouldNotBe_Deleted]} : {exception.Message}");
            }
        }

        public async Task<IDataResult<BookDto>> UpdateAsync(BookUpdateDto bookUpdateDto)
        {
            try
            {
                var book = await bookRepository.GetByIdAsync(bookUpdateDto.Id);
                if (book is null) return new ErrorDataResult<BookDto>(stringLocalizer[Messages.Book_Was_Not_Found]);

                await bookRepository.UpdateAsync(bookUpdateDto.Adapt(book));
                await unitOfWork.SaveChangesAsync();
                await cacheService.SetCacheAsync($"Book_{book.Id}", book, TimeSpan.FromDays(30));

                return new SuccessDataResult<BookDto>(stringLocalizer[Messages.Book_HasBeen_Updated]);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return new ErrorDataResult<BookDto>($"{stringLocalizer[Messages.Book_CouldNotBe_Updated]} : {exception.Message}");
            }
        }

        public async Task<IDataResult<BookDto>> GetByIdAsync(Guid bookId)
        {
            try
            {
                var result = await cacheService.GetCacheAsync($"Book_{bookId}");
                if (result.Data is not null) return new SuccessDataResult<BookDto>(result.Data.Adapt<BookDto>(), stringLocalizer[Messages.Book_Was_Found_Successfully]);

                var book = await bookRepository.GetByIdAsync(bookId);
                if (book is null) return new ErrorDataResult<BookDto>($"{stringLocalizer[Messages.Book_Was_Not_Found]}");


                await cacheService.SetCacheAsync($"Book_{book.Id}", book, TimeSpan.FromDays(30));
                return new SuccessDataResult<BookDto>(book.Adapt<BookDto>(), stringLocalizer[Messages.Book_Was_Found_Successfully]);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return new ErrorDataResult<BookDto>($"{stringLocalizer[Messages.Book_Was_Not_Found]} : {exception.Message}");
            }
        }

        public async Task<IDataResult<List<BookListDto>>> GetAllAsync()
        {
            try
            {
                var books = await bookRepository.GetAllAsync();
                var bookListDtos = books.Adapt<List<BookListDto>>() ?? new List<BookListDto>();

                return new SuccessDataResult<List<BookListDto>>(bookListDtos, stringLocalizer[Messages.Book_GetAllProcess_Was_Successful]);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return new ErrorDataResult<List<BookListDto>>($"{stringLocalizer[Messages.Book_GetAllProcess_Was_Failed]} : {exception.Message}");
            }
        }
    }
}