namespace Library.Dtos.BookDtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public int PublicationYear { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public string Summary { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}