using Microsoft.EntityFrameworkCore;
using PolicyWatcher.API.Middlewares;
using PolicyWatcher.Application.Services;
using PolicyWatcher.Domain.Interfaces.Repository;
using PolicyWatcher.Domain.Interfaces.Service;
using PolicyWatcher.Infrastructure.Data;
using PolicyWatcher.Infrastructure.Repositories;

namespace PolicyWatcher.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddDbContext<PolicyWatcherDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            return services;
        }
    }

}
