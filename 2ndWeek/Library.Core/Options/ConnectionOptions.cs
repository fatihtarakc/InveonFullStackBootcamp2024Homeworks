namespace Library.Core.Options
{
    public class ConnectionOptions
    {
        public const string Connections = "Connections";

        public string MssqlServer { get; set; }
        public string Hangfire { get; set; }
        public string Rabbitmq { get; set; }
        public string Redis { get; set; }
    }
}