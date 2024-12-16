namespace Library.Core.Options
{
    public class ConnectionOptions
    {
        public const string Connections = "ConnectionStrings";

        public string MssqlServerConnectionString { get; set; }
        public string HangfireConnectionString { get; set; }
        public string RedisConnectionString { get; set; }
    }
}