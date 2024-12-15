namespace Library.Business.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions emailOptions;
        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            this.emailOptions = emailOptions.Value;
        }

        private async Task<MimeMessage> CreateEmailContentAsync(EmailDto emailDto, EmailOptions emailOptions)
        {
            MailboxAddress mailboxAddressFrom = new(emailOptions.From, emailOptions.EmailFrom);
            MailboxAddress mailboxAddressTo = new(emailDto.To, emailDto.EmailTo);

            BodyBuilder bodyBuilder = new();
            bodyBuilder.HtmlBody = emailDto.Content;

            MimeMessage mimeMessage = new();
            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);
            mimeMessage.Subject = emailDto.Title + " " + emailDto.Subject;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            return await Task.FromResult(mimeMessage);
        }

        public async Task SendAsync(EmailDto emailDto)
        {
            var mimeMessage = await CreateEmailContentAsync(emailDto, emailOptions);

            using SmtpClient smtpClient = new();
            try
            {
                await smtpClient.ConnectAsync(emailOptions.SmtpServer, emailOptions.Port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(emailOptions.Username, emailOptions.Password);
                await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Email gönderilirken bir hata oluştu : {exception.Message}");
            }
        }
    }
}