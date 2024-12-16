namespace Library.Queue.Services.Abstract
{
    public interface IRabbitmqConsumerService
    {
        Task StartSendingEmailForNewAppUserAsync();

        Task StartSendingEmailForEmailVerificationCodeAsync();

        Task StartSendingEmailForPasswordChangeVerificationCodeAsync();

        Task StartSendingEmailForTwoFactorAuthenticationVerificationCodeAsync();
    }
}