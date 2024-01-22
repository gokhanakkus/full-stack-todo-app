using Todo.Services.Models.Auth;
using Todo.Shared.Service;

namespace Todo.Services.Abstract
{
    public interface IAuthService
    {
        Task<IServiceResult<TokenResponseModel>> LoginAsync(string username, string password, CancellationToken cancellationToken);
    }
}
