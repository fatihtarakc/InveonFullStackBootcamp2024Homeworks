namespace Library.Queue.Services.Abstract
{
    public interface IRabbitmqPublisherService
    {
        void EnqueueModelAsync<T>(T queueDataModel, string queueName) where T : class, new();

        void EnqueueModelsAsync<T>(IEnumerable<T> queueDataModels, string queueName) where T : class, new();
    }
}