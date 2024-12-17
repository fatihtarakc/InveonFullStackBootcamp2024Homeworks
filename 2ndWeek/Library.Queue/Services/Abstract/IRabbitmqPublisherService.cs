namespace Library.Queue.Services.Abstract
{
    public interface IRabbitmqPublisherService
    {
        Task<IResult> EnqueueModelAsync<T>(T queueDataModel, string queueName) where T : class, new();

        Task<IResult> EnqueueModelsAsync<T>(IEnumerable<T> queueDataModels, string queueName) where T : class, new();
    }
}