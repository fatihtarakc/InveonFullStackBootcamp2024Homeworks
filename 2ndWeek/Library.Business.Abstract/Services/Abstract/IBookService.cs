namespace Library.Business.Abstract.Services.Abstract
{
    public interface IBookService
    {
        Task<IDataResult<BookDto>> AddAsync(BookAddDto bookAddDto, string username);

        Task<IResult> DeleteAsync(Guid bookId);

        Task<IDataResult<BookDto>> UpdateAsync(BookUpdateDto bookUpdateDto);

        Task<IDataResult<BookDto>> GetByIdAsync(Guid bookId);

        Task<IDataResult<List<BookListDto>>> GetAllAsync();
    }
}