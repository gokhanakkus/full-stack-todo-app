using Microsoft.Extensions.DependencyInjection;
using Todo.Data;
using Todo.Data.Models;
using Todo.Services.Abstract;

namespace Todo.Services.Concrete.Api
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, string connectionString)
        {
            services.AddTodoDbContext(connectionString);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDataRepository<UserEntity>, DataRepository<UserEntity>>();
            services.AddScoped<IDataRepository<TodoEntity>, DataRepository<TodoEntity>>();

            return services;
        }
    }
}
