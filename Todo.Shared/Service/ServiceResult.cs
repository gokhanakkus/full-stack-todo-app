using Microsoft.AspNetCore.Http;

namespace Todo.Shared.Service
{
    public class ServiceResult : IServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }

        public static ServiceResult Success(string message = "", int statusCode = StatusCodes.Status200OK) =>
            new() { IsSuccess = true, Message = message, StatusCode = statusCode };

        public static ServiceResult<T> Success<T>(T? data, string message = "", int statusCode = StatusCodes.Status200OK) =>
            new() { IsSuccess = true, Data = data, Message = message, StatusCode = statusCode };

        public static ServiceResult Fail(string message = "", int statusCode = StatusCodes.Status400BadRequest) =>
            new() { IsSuccess = false, Message = message, StatusCode = statusCode };

        public static ServiceResult<T> Fail<T>(string message = "", int statusCode = StatusCodes.Status400BadRequest) =>
            new() { IsSuccess = false, Message = message, StatusCode = statusCode };
    }

    public class ServiceResult<T> : ServiceResult, IServiceResult<T>
    {
        public T? Data { get; set; }
    }
}
