namespace Library.Business.Abstract.Services.Abstract
{
    public interface IEmailService
    {
        Task<IResult> SendingEmailForNewAppUserAsync(EmailForNewAppUserDto emailForNewAppUserDto);

        Task<IResult> SendingEmailForEmailVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto);

        Task<IResult> SendingEmailForPasswordChangeVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto);

        Task<IResult> SendingEmailForTwoFactorAuthenticationVerificationCodeAsync(EmailForVerificationCodeDto emailForVerificationCodeDto);
    }
}