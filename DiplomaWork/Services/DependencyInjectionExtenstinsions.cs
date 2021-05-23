using DiplomaWork1.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiplomaWork1.Services
{
    public static class DependencyInjectionExtenstinsions
    {
        public static IServiceCollection AddServices(this IServiceCollection collection)
        {
            collection.AddScoped<IRequestService, RequestService>();

            return collection;
        }
    }
}
