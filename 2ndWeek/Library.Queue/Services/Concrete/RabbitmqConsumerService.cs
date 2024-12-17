namespace Library.Queue.Services.Concrete
{
    public class RabbitmqConsumerService : IRabbitmqConsumerService
    {
        private readonly IRabbitmqService rabbitmqService;
        private readonly IEmailService emailService;
        private readonly IObjectConvertFormatService objectConvertFormatService;
        private readonly IStringLocalizer<MessageResources> stringLocalizer;
        private readonly ILogger<RabbitmqConsumerService> logger;
        public RabbitmqConsumerService(IRabbitmqService rabbitmqService, IEmailService emailService, IObjectConvertFormatService objectConvertFormatService, IStringLocalizer<MessageResources> stringLocalizer, ILogger<RabbitmqConsumerService> logger)
        {
            this.rabbitmqService = rabbitmqService;
            this.emailService = emailService;
            this.objectConvertFormatService = objectConvertFormatService;
            this.stringLocalizer = stringLocalizer;
            this.logger = logger;
        }

        private async Task<IResult> StartSendingEmailAsync(AsyncEventHandler<BasicDeliverEventArgs> asyncEventHandler, string queueName)
        {
            try
            {
                var channel = await rabbitmqService.CreateChannelAsync();
                await channel.QueueDeclareAsync(queue: queueName,
                        durable: true, exclusive: false, autoDelete: false, arguments: null);

                var eventingBasicConsumer = new AsyncEventingBasicConsumer(channel);
                eventingBasicConsumer.ReceivedAsync += asyncEventHandler;
                await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: eventingBasicConsumer);
                return new SuccessResult(stringLocalizer[Messages.Rabbitmq_StartSendingEmailProcess_Was_Successful]);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return new ErrorResult($"{stringLocalizer[Messages.Rabbitmq_StartSendingEmailProcess_Was_Failed]} : {exception.Message}");
            }
        }

        public async Task<IResult> StartSendingEmailForNewAppUserAsync()
        {
            var result = await StartSendingEmailAsync(asyncEventHandler: ConsumerReceivedEmailForNewAppUserAsync, queueName: QueueNames.NewAppUser);
            if (result.IsSuccess) return new SuccessResult(result.Message);

            return new ErrorResult(result.Message);
        }
        private async Task<IResult> ConsumerReceivedEmailForNewAppUserAsync(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var emailForNewAppUserDto = objectConvertFormatService.ToObjectFromJson<EmailForNewAppUserDto>(Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray()));
            var result = await emailService.SendingEmailForNewAppUserAsync(emailForNewAppUserDto);
            if (!result.IsSuccess) return new ErrorResult(result.Message);

            Console.WriteLine($"New AppUser - {emailForNewAppUserDto.To} : It was sent email to  {emailForNewAppUserDto.EmailTo} ! \n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
            return new SuccessResult(result.Message);
        }

        public async Task<IResult> StartSendingEmailForEmailVerificationCodeAsync()
        {
            var result = await StartSendingEmailAsync(asyncEventHandler: ConsumerReceivedEmailForEmailVerificationCodeAsync, queueName: QueueNames.EmailVerificationCode);
            if (result.IsSuccess) return new SuccessResult(result.Message);

            return new ErrorResult(result.Message);
        }
        private async Task<IResult> ConsumerReceivedEmailForEmailVerificationCodeAsync(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var emailForVerificationCodeDto = objectConvertFormatService.ToObjectFromJson<EmailForVerificationCodeDto>(Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray()));
            var result = await emailService.SendingEmailForEmailVerificationCodeAsync(emailForVerificationCodeDto);
            if (!result.IsSuccess) return new ErrorResult(result.Message);

            Console.WriteLine($"AppUser - {emailForVerificationCodeDto.To} : {emailForVerificationCodeDto.VerificationCode} verification code for email verification was sent email to {emailForVerificationCodeDto.EmailTo} ! \n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
            return new SuccessResult(result.Message);
        }

        public async Task<IResult> StartSendingEmailForPasswordChangeVerificationCodeAsync()
        {
            var result = await StartSendingEmailAsync(asyncEventHandler: ConsumerReceivedEmailForPasswordChangeVerificationCodeAsync, queueName: QueueNames.PasswordChangeVerificationCode);
            if (result.IsSuccess) return new SuccessResult(result.Message);

            return new ErrorResult(result.Message);
        }
        private async Task<IResult> ConsumerReceivedEmailForPasswordChangeVerificationCodeAsync(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var emailForVerificationCodeDto = objectConvertFormatService.ToObjectFromJson<EmailForVerificationCodeDto>(Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray()));
            var result = await emailService.SendingEmailForPasswordChangeVerificationCodeAsync(emailForVerificationCodeDto);
            if (!result.IsSuccess) return new ErrorResult(result.Message);

            Console.WriteLine($"AppUser - {emailForVerificationCodeDto.To} : {emailForVerificationCodeDto.VerificationCode} verification code for password change was sent email to {emailForVerificationCodeDto.EmailTo} ! \n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
            return new SuccessResult(result.Message);
        }

        public async Task<IResult> StartSendingEmailForTwoFactorAuthenticationVerificationCodeAsync()
        {
            var result = await StartSendingEmailAsync(asyncEventHandler: ConsumerReceivedEmailForTwoFactorAuthenticationVerificationCodeAsync, queueName: QueueNames.TwoFactorAuthenticationVerificationCode);
            if (result.IsSuccess) return new SuccessResult(result.Message);

            return new ErrorResult(result.Message);
        }
        private async Task<IResult> ConsumerReceivedEmailForTwoFactorAuthenticationVerificationCodeAsync(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var emailForVerificationCodeDto = objectConvertFormatService.ToObjectFromJson<EmailForVerificationCodeDto>(Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray()));
            var result = await emailService.SendingEmailForTwoFactorAuthenticationVerificationCodeAsync(emailForVerificationCodeDto);
            if (!result.IsSuccess) return new ErrorResult(result.Message);

            Console.WriteLine($"AppUser - {emailForVerificationCodeDto.To} : {emailForVerificationCodeDto.VerificationCode} verification code for two factor authentication was sent email to {emailForVerificationCodeDto.EmailTo} ! \n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
            return new SuccessResult(result.Message);
        }
    }
}