using Todo.Shared.Service;

namespace Todo.Services.Abstract
{
    public interface IApiService
    {
        Task<IServiceResult<TModel>> GetAsync<TModel>(string url, CancellationToken cancellationToken);
        Task<IServiceResult<TModel>> PostAsync<TModel>(string url, TModel data, CancellationToken cancellationToken);
        Task<IServiceResult<TModel>> PutAsync<TModel>(string url, TModel data, CancellationToken cancellationToken);
        Task<IServiceResult> DeleteAsync(string url, CancellationToken cancellationToken);
    }
}
