using gmltec.application.Contracts.Services;
using gmltec.application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace gmltec.application
{
    public static class ConfigurationServices
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

    }
}
