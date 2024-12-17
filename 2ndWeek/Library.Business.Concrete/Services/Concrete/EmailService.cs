namespace Library.Business.Concrete.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions emailOptions;
        private readonly IStringLocalizer<MessageResources> stringLocalizer;
        private readonly ILogger<EmailService> logger;
        public EmailService(IOptions<EmailOptions> emailOptions, IStringLocalizer<MessageResources> stringLocalizer, ILogger<EmailService> logger)
        {
            this.emailOptions = emailOptions.Value;
            this.stringLocalizer = stringLocalizer;
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
        private async Task<IResult> SendAsync(EmailDto emailDto)
        {
            var mimeMessage = await CreateEmailContentAsync(emailDto, emailOptions);

            using SmtpClient smtpClient = new();
            try
            {
                await smtpClient.ConnectAsync(emailOptions.SmtpServer, emailOptions.Port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(emailOptions.Username, emailOptions.Password);
                await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);

                return new SuccessResult(stringLocalizer[Messages.Email_SendingProcess_Was_Successful]);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return new ErrorResult($"{stringLocalizer[Messages.Email_SendingProcess_Was_Failed]} : {exception.Message}");
            }
        }

        public async Task<IResult> SendingEmailForNewAppUserAsync(EmailForNewAppUserDto emailForNewAppUserDto)
        {
            var result = await SendAsync(new EmailDto(emailForNewAppUserDto.To, emailForNewAppUserDto.EmailTo, $"{stringLocalizer[Messages.EmailTitle_Has_Been_Sent_For_NewAppUser]} {emailForNewAppUserDto.To},", stringLocalizer[Messages.EmailSubject_Has_Been_Sent_For_NewAppUser], $"{stringLocalizer[Messages.EmailContent_Has_Been_Sent_For_NewAppUser]}\n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}"));
            if (result.IsSuccess) return new SuccessResult(result.Message);

            return new ErrorResult(result.Message);
        }

        public async Task<IResult> SendingEmailForEmailVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto)
        {
            var result = await SendAsync(new EmailDto(emailForVerificationCodeDto.To, emailForVerificationCodeDto.EmailTo, $"{stringLocalizer[Messages.EmailTitle_Has_Been_Sent_For_EmailVerificationCode]} {emailForVerificationCodeDto.To},", stringLocalizer[Messages.EmailSubject_Has_Been_Sent_For_EmailVerificationCode], $"{stringLocalizer[Messages.EmailContent_Has_Been_Sent_For_EmailVerificationCode]}: {emailForVerificationCodeDto.VerificationCode}\n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}"));
            if (result.IsSuccess) return new SuccessResult(result.Message);

            return new ErrorResult(result.Message);
        }

        public async Task<IResult> SendingEmailForPasswordChangeVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto)
        {
            var result = await SendAsync(new EmailDto(emailForVerificationCodeDto.To, emailForVerificationCodeDto.EmailTo, $"{stringLocalizer[Messages.EmailTitle_Has_Been_Sent_For_PasswordChangeVerificationCode]} {emailForVerificationCodeDto.To},", stringLocalizer[Messages.EmailSubject_Has_Been_Sent_For_PasswordChangeVerificationCode], $"{stringLocalizer[Messages.EmailContent_Has_Been_Sent_For_PasswordChangeVerificationCode]}: {emailForVerificationCodeDto.VerificationCode}\n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}"));
            if (result.IsSuccess) return new SuccessResult(result.Message);

            return new ErrorResult(result.Message);
        }

        public async Task<IResult> SendingEmailForTwoFactorAuthenticationVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto)
        {
            var result = await SendAsync(new EmailDto(emailForVerificationCodeDto.To, emailForVerificationCodeDto.EmailTo, $"{stringLocalizer[Messages.EmailTitle_Has_Been_Sent_For_TwoFactorAuthenticationVerificationCode]} {emailForVerificationCodeDto.To},", stringLocalizer[Messages.EmailSubject_Has_Been_Sent_For_TwoFactorAuthenticationVerificationCode], $"{stringLocalizer[Messages.EmailContent_Has_Been_Sent_For_TwoFactorAuthenticationVerificationCode]}: {emailForVerificationCodeDto.VerificationCode}\n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}"));
            if (result.IsSuccess) return new SuccessResult(result.Message);

            return new ErrorResult(result.Message);
        }
    }
}