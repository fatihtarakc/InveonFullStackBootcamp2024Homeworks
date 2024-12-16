namespace Library.Queue.Services.Abstract
{
    public interface IObjectConvertFormatService
    {
        string ToJsonFromObject<T>(T entityObject) where T : class, new();
        T ToObjectFromJson<T>(string jsonString) where T : class, new();
        T ToObjectParseDataArray<T>(byte[] rawBytes) where T : class, new();
    }
}