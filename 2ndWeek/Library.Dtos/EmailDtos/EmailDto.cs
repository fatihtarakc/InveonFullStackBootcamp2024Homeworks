namespace Library.Dtos.EmailDtos
{
    public class EmailDto
    {
        public EmailDto(string to, string emailTo, string title, string subject, string content)
        {
            To = to;
            EmailTo = emailTo;
            Title = title;
            Subject = subject;
            Content = content;
        }

        public string To { get; set; }
        public string EmailTo { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}