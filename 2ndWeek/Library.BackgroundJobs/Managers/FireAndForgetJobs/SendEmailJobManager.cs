namespace Library.BackgroundJobs.Managers.FireAndForgetJobs
{
    public class SendEmailJobManager
    {
        private readonly IRabbitmqConsumerService rabbitmqConsumerService;
        public SendEmailJobManager(IRabbitmqConsumerService rabbitmqConsumerService)
        {
            this.rabbitmqConsumerService = rabbitmqConsumerService;
        }

        public async Task ExecuteAsync()
        {
            await rabbitmqConsumerService.StartSendingEmailForNewAppUserAsync();
            await rabbitmqConsumerService.StartSendingEmailForEmailVerificationCodeAsync();
            await rabbitmqConsumerService.StartSendingEmailForPasswordChangeVerificationCodeAsync();
            await rabbitmqConsumerService.StartSendingEmailForTwoFactorAuthenticationVerificationCodeAsync();
        }
    }
}