namespace Section3.LibraryManagementSystem.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Author { get; set; }
    }
}