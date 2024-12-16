namespace Library.Queue.Services.Concrete
{
    public class RabbitmqPublisherService : IRabbitmqPublisherService
    {
        private readonly IRabbitmqService rabbitmqService;
        public RabbitmqPublisherService(IRabbitmqService rabbitmqService)
        {
            this.rabbitmqService = rabbitmqService;
        }

        public async void EnqueueModelAsync<T>(T queueDataModel, string queueName) where T : class, new()
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
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public async void EnqueueModelsAsync<T>(IEnumerable<T> queueDataModels, string queueName) where T : class, new()
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
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }
    }
}