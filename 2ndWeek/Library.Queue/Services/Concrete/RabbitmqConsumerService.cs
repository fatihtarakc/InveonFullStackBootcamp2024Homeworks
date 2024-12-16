namespace Library.Queue.Services.Concrete
{
    public class RabbitmqConsumerService : IRabbitmqConsumerService
    {
        private readonly IRabbitmqService rabbitmqService;
        private readonly IEmailService emailService;
        private readonly IObjectConvertFormatService objectConvertFormatService;
        private readonly ILogger<RabbitmqConsumerService> logger;
        public RabbitmqConsumerService(IRabbitmqService rabbitmqService, IEmailService emailService, IObjectConvertFormatService objectConvertFormatService, ILogger<RabbitmqConsumerService> logger)
        {
            this.rabbitmqService = rabbitmqService;
            this.emailService = emailService;
            this.objectConvertFormatService = objectConvertFormatService;
            this.logger = logger;
        }

        private async Task StartSendingEmailAsync(AsyncEventHandler<BasicDeliverEventArgs> asyncEventHandler, string queueName)
        {
            try
            {
                var channel = await rabbitmqService.CreateChannelAsync();
                await channel.QueueDeclareAsync(queue: queueName,
                        durable: true, exclusive: false, autoDelete: false, arguments: null);

                var eventingBasicConsumer = new AsyncEventingBasicConsumer(channel);
                eventingBasicConsumer.ReceivedAsync += asyncEventHandler;
                await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: eventingBasicConsumer);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                throw new Exception(exception.Message);
            }
        }

        public async Task StartSendingEmailForNewAppUserAsync() =>
            await StartSendingEmailAsync(asyncEventHandler: ConsumerReceivedEmailForNewAppUserAsync, queueName: QueueNames.NewAppUser);
        private async Task ConsumerReceivedEmailForNewAppUserAsync(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var emailForNewAppUserDto = objectConvertFormatService.ToObjectFromJson<EmailForNewAppUserDto>(Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray()));
            await emailService.SendingEmailForNewAppUserAsync(emailForNewAppUserDto);
            Console.WriteLine($"New AppUser - {emailForNewAppUserDto.To} : It was sent email to  {emailForNewAppUserDto.EmailTo} ! \n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
        }

        public async Task StartSendingEmailForEmailVerificationCodeAsync() =>
            await StartSendingEmailAsync(asyncEventHandler: ConsumerReceivedEmailForEmailVerificationCodeAsync, queueName: QueueNames.EmailVerificationCode);
        private async Task ConsumerReceivedEmailForEmailVerificationCodeAsync(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var emailForVerificationCodeDto = objectConvertFormatService.ToObjectFromJson<EmailForVerificationCodeDto>(Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray()));
            await emailService.SendingEmailForEmailVerificationCodeAsync(emailForVerificationCodeDto);
            Console.WriteLine($"AppUser - {emailForVerificationCodeDto.To} : {emailForVerificationCodeDto.VerificationCode} verification code for email verification was sent email to {emailForVerificationCodeDto.EmailTo} ! \n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
        }

        public async Task StartSendingEmailForPasswordChangeVerificationCodeAsync() =>
            await StartSendingEmailAsync(asyncEventHandler: ConsumerReceivedEmailForPasswordChangeVerificationCodeAsync, queueName: QueueNames.PasswordChangeVerificationCode);
        private async Task ConsumerReceivedEmailForPasswordChangeVerificationCodeAsync(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var emailForVerificationCodeDto = objectConvertFormatService.ToObjectFromJson<EmailForVerificationCodeDto>(Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray()));
            await emailService.SendingEmailForPasswordChangeVerificationCodeAsync(emailForVerificationCodeDto);
            Console.WriteLine($"AppUser - {emailForVerificationCodeDto.To} : {emailForVerificationCodeDto.VerificationCode} verification code for password change was sent email to {emailForVerificationCodeDto.EmailTo} ! \n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
        }

        public async Task StartSendingEmailForTwoFactorAuthenticationVerificationCodeAsync() =>
            await StartSendingEmailAsync(asyncEventHandler: ConsumerReceivedEmailForTwoFactorAuthenticationVerificationCodeAsync, queueName: QueueNames.TwoFactorAuthenticationVerificationCode);
        private async Task ConsumerReceivedEmailForTwoFactorAuthenticationVerificationCodeAsync(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var emailForVerificationCodeDto = objectConvertFormatService.ToObjectFromJson<EmailForVerificationCodeDto>(Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray()));
            await emailService.SendingEmailForTwoFactorAuthenticationVerificationCodeAsync(emailForVerificationCodeDto);
            Console.WriteLine($"AppUser - {emailForVerificationCodeDto.To} : {emailForVerificationCodeDto.VerificationCode} verification code for two factor authentication was sent email to {emailForVerificationCodeDto.EmailTo} ! \n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
        }
    }
}