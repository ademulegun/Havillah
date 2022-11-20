using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Havillah.ApplicationServices.Extensions;

public static class Dependencies
{
    public static void AddMediatR(this IServiceCollection services)
    {
        //services.AddMediatR(typeof(ProductByIdQuery));
    }
}