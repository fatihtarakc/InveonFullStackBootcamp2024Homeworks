namespace Library.Queue.Services.Abstract
{
    public interface IRabbitmqService
    {
        Task<IChannel> CreateChannelAsync();
    }
}