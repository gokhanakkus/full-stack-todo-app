using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Data.Context;

namespace Todo.Data
{
    public static class DataExtensions
    {
        public static void AddTodoDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DbContext, AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
