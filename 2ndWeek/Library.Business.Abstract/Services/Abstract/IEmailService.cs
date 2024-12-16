namespace Library.Business.Abstract.Services.Abstract
{
    public interface IEmailService
    {
        Task SendAsync(EmailDto emailDto);

        Task SendingEmailForNewAppUserAsync(EmailForNewAppUserDto emailForNewAppUserDto);

        Task SendingEmailForEmailVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto);

        Task SendingEmailForPasswordChangeVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto);

        Task SendingEmailForTwoFactorAuthenticationVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto);
    }
}