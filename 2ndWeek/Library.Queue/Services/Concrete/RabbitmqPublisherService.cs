namespace Library.Queue.Services.Concrete
{
    public class RabbitmqPublisherService : IRabbitmqPublisherService
    {
        private readonly IRabbitmqService rabbitmqService;
        private readonly IStringLocalizer<MessageResources> stringLocalizer;
        private readonly ILogger<RabbitmqPublisherService> logger;
        public RabbitmqPublisherService(IRabbitmqService rabbitmqService, IStringLocalizer<MessageResources> stringLocalizer, ILogger<RabbitmqPublisherService> logger)
        {
            this.rabbitmqService = rabbitmqService;
            this.stringLocalizer = stringLocalizer;
            this.logger = logger;
        }

        public async Task<IResult> EnqueueModelAsync<T>(T queueDataModel, string queueName) where T : class, new()
        {
            try
            {
                using (var channel = await rabbitmqService.CreateChannelAsync())
                {
                    await channel.QueueDeclareAsync(queue: queueName,
                        durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(queueDataModel));
                    await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
                }

                return new SuccessResult(stringLocalizer[Messages.Rabbitmq_EnqueueModelProcess_Was_Successful]);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return new ErrorResult($"{stringLocalizer[Messages.Rabbitmq_EnqueueModelProcess_Was_Successful]} : {exception.Message}");
            }
        }

        public async Task<IResult> EnqueueModelsAsync<T>(IEnumerable<T> queueDataModels, string queueName) where T : class, new()
        {
            try
            {
                using (var channel = await rabbitmqService.CreateChannelAsync())
                {
                    await channel.QueueDeclareAsync(queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    foreach (var queueDataModel in queueDataModels)
                    {
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(queueDataModel));
                        await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
                    }
                }

                return new SuccessResult(stringLocalizer[Messages.Rabbitmq_EnqueueModelsProcess_Were_Successful]);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return new ErrorResult($"{stringLocalizer[Messages.Rabbitmq_EnqueueModelsProcess_Were_Failed]} : {exception.Message}");
            }
        }
    }
}