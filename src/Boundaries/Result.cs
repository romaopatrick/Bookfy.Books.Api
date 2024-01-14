namespace Bookfy.Books.Api.Boundaries
{
    public class Result<T> : Result
    {
        public T? Data { get; init; }
        
    }

    public class Result {

        public required bool Success { get; init; }
        public int Code { get; init; }
        public string? Message { get; init; }

        public static Result<T> WithSuccess<T>(T data, int code) => new()
        {
            Code = code,
            Data = data,
            Success = true
        };

        public static Result WithSuccess(int code) => new() 
        {
            Success = true,
            Code = code
        };

        public static Result<T> WithFailure<T>(string message, int code) => new()
        {
            Success = false,
            Code = code,
            Message = message
        };
        public static Result WithFailure(string message, int code) => new()
        {
            Success = false,
            Code = code,
            Message = message
        };
    }
}