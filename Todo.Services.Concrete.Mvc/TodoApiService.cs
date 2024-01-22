using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using Todo.Services.Abstract;
using Todo.Shared.Service;

namespace Todo.Services.Concrete.Mvc
{
    public class TodoApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TodoApiService> _logger;

        public TodoApiService(IHttpClientFactory clientFactory, IConfiguration config,
            ILogger<TodoApiService> logger)
        {
            _httpClient = clientFactory.CreateClient("TodoApiService");
            _configuration = config;
            _logger = logger;
        }

        public async Task<IServiceResult> DeleteAsync(string url, CancellationToken cancellationToken) =>
            await MakeRequest<string>(url, HttpMethod.Delete, default, cancellationToken);

        public async Task<IServiceResult<TModel>> GetAsync<TModel>(string url, CancellationToken cancellationToken) =>
            await MakeRequest<TModel>(url, HttpMethod.Get, default, cancellationToken);

        public async Task<IServiceResult<TModel>> PostAsync<TModel>(string url, TModel data, CancellationToken cancellationToken) =>
            await MakeRequest(url, HttpMethod.Post, data, cancellationToken);

        public async Task<IServiceResult<TModel>> PutAsync<TModel>(string url, TModel data, CancellationToken cancellationToken) =>
            await MakeRequest(url, HttpMethod.Put, data, cancellationToken);

        private async Task<IServiceResult<TModel>> MakeRequest<TModel>(string url, HttpMethod method, TModel? requestData, CancellationToken cancellationToken)
        {
            try
            {
                var request = new HttpRequestMessage(method, url);
                if (requestData != null)
                {
                    request.Content = JsonContent.Create(requestData);
                }
                var response = await _httpClient.SendAsync(request, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    return ServiceResult.Fail<TModel>(statusCode: (int)response.StatusCode);
                }

                var responseText = await response.Content.ReadAsStringAsync(cancellationToken);
                if (!string.IsNullOrWhiteSpace(responseText))
                {
                    var responseData = JsonSerializer.Deserialize<TModel>(responseText);
                    if (responseData != null)
                    {
                        return ServiceResult.Success(responseData);
                    }
                }

                return ServiceResult.Success<TModel>(default);
            }
            catch (OperationCanceledException ocex)
            {
                _logger.LogWarning(ocex, "Operation cancelled.");
                return ServiceResult.Fail<TModel>("Operation cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return ServiceResult.Fail<TModel>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}