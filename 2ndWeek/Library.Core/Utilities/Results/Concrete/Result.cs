namespace Library.Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result()
        {
            IsSuccess = false;
            Message = string.Empty;
        }

        public Result(bool isSuccess) : this()
        {
            IsSuccess = isSuccess;
        }

        public Result(bool isSuccess, string message) : this(isSuccess)
        {
            Message = message;
        }

        public bool IsSuccess { get; }
        public string Message { get; }
    }
}