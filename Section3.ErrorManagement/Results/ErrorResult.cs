using System.Text.Json;

namespace Section3.ErrorManagement.Results
{
    public class ErrorResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}