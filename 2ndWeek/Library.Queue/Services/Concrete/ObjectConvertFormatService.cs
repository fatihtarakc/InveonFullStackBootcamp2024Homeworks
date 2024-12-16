namespace Library.Queue.Services.Concrete
{
    public class ObjectConvertFormatService : IObjectConvertFormatService
    {
        public string ToJsonFromObject<T>(T entityObject) where T : class, new() =>
            JsonConvert.SerializeObject(entityObject);

        public T ToObjectFromJson<T>(string jsonString) where T : class, new() =>
            JsonConvert.DeserializeObject<T>(jsonString);

        public T ToObjectParseDataArray<T>(byte[] rawBytes) where T : class, new() =>
            ToObjectFromJson<T>(Encoding.UTF8.GetString(rawBytes));
    }
}