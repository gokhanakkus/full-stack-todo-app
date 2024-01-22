namespace Todo.Shared.Service
{
    public interface IServiceResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
        int StatusCode { get; set; }
    }

    public interface IServiceResult<T> : IServiceResult
    {
        T? Data { get; set; }
    }
}
