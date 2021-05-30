using DiplomaWork.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiplomaWork.Services
{
    public static class DependencyInjectionExtenstinsions
    {
        public static IServiceCollection AddServices(this IServiceCollection collection)
        {
            collection.AddScoped<IRequestService, RequestService>();
            collection.AddTransient<IRequestNotifier, RequestNotifier>();

            return collection;
        }
    }
}
