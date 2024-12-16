namespace Library.Queue.Services.Concrete
{
    public class RabbitmqService : IRabbitmqService
    {
        private readonly ConnectionOptions connectionOptions;
        public RabbitmqService(IOptions<ConnectionOptions> connectionOptions)
        {
            this.connectionOptions = connectionOptions.Value;
        }

        public async Task<IChannel> CreateChannelAsync()
        {
            try
            {
                var factory = new ConnectionFactory { Uri = new Uri(connectionOptions.Rabbitmq) };
                factory.AutomaticRecoveryEnabled = true;  // Otomatik bağlantıyı etkinleştirmek için.
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);  // Her 10 saniye de bir tekrar bağlanmaya çalışır.
                factory.TopologyRecoveryEnabled = false;  // Sunucudan bağlantısı kesildikten sonra kuyruktaki mesaj tüketimini sürdürmez.
                var connection = await factory.CreateConnectionAsync();
                return await connection.CreateChannelAsync();
            }
            catch
            {
                Thread.Sleep(5000);
                return await CreateChannelAsync();
            }
        }
    }
}