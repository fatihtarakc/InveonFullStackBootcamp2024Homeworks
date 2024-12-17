namespace Library.Queue.Services.Abstract
{
    public interface IRabbitmqConsumerService
    {
        Task<IResult> StartSendingEmailForNewAppUserAsync();

        Task<IResult> StartSendingEmailForEmailVerificationCodeAsync();

        Task<IResult> StartSendingEmailForPasswordChangeVerificationCodeAsync();

        Task<IResult> StartSendingEmailForTwoFactorAuthenticationVerificationCodeAsync();
    }
}