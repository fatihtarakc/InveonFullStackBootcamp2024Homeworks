namespace Section3.LibraryManagementSystem.Dtos.BookDtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Author { get; set; }
    }
}