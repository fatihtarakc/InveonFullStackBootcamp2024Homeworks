namespace Library.Business.Concrete.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions emailOptions;
        private readonly ILogger<EmailService> logger;
        public EmailService(IOptions<EmailOptions> emailOptions, ILogger<EmailService> logger)
        {
            this.emailOptions = emailOptions.Value;
            this.logger = logger;
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
                logger.LogError(exception.Message);
                throw new InvalidOperationException($"Email gönderilirken bir hata oluştu : {exception.Message}");
            }
        }

        public async Task SendingEmailForNewAppUserAsync(EmailForNewAppUserDto emailForNewAppUserDto) =>
            await SendAsync(new EmailDto(emailForNewAppUserDto.To, emailForNewAppUserDto.EmailTo, $"{Message.EmailTitle_Has_Been_Sent_For_NewAppUser} {emailForNewAppUserDto.To},", Message.EmailSubject_Has_Been_Sent_For_NewAppUser, $"{Message.EmailContent_Has_Been_Sent_For_NewAppUser}\n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}"));

        public async Task SendingEmailForEmailVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto) =>
            await SendAsync(new EmailDto(emailForVerificationCodeDto.To, emailForVerificationCodeDto.EmailTo, $"{Message.EmailTitle_Has_Been_Sent_For_EmailVerificationCode} {emailForVerificationCodeDto.To},", Message.EmailSubject_Has_Been_Sent_For_EmailVerificationCode, $"{Message.EmailContent_Has_Been_Sent_For_EmailVerificationCode}: {emailForVerificationCodeDto.VerificationCode}\n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}"));

        public async Task SendingEmailForPasswordChangeVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto) =>
            await SendAsync(new EmailDto(emailForVerificationCodeDto.To, emailForVerificationCodeDto.EmailTo, $"{Message.EmailTitle_Has_Been_Sent_For_PasswordChangeVerificationCode} {emailForVerificationCodeDto.To},", Message.EmailSubject_Has_Been_Sent_For_PasswordChangeVerificationCode, $"{Message.EmailContent_Has_Been_Sent_For_PasswordChangeVerificationCode}: {emailForVerificationCodeDto.VerificationCode}\n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}"));

        public async Task SendingEmailForTwoFactorAuthenticationVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto) =>
            await SendAsync(new EmailDto(emailForVerificationCodeDto.To, emailForVerificationCodeDto.EmailTo, $"{Message.EmailTitle_Has_Been_Sent_For_TwoFactorAuthenticationVerificationCode} {emailForVerificationCodeDto.To},", Message.EmailSubject_Has_Been_Sent_For_TwoFactorAuthenticationVerificationCode, $"{Message.EmailContent_Has_Been_Sent_For_TwoFactorAuthenticationVerificationCode}: {emailForVerificationCodeDto.VerificationCode}\n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}"));
    }
}