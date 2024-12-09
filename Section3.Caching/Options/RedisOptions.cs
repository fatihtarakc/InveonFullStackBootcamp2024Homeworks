namespace Section3.Caching.Options
{
    public class RedisOptions
    {
        public const string Redis = "Redis";

        public string Host { get; set; }
        public int Port { get; set; }
    }
}