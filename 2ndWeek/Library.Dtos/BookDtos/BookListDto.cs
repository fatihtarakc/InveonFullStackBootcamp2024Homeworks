namespace Library.Dtos.BookDtos
{
    public class BookListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}