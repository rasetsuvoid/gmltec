using gmltec.application.Contracts.Persistence;
using gmltec.application.Contracts.UoW;
using gmltec.Infrastructure.Repositories;
using gmltec.Infrastructure.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace gmltec.Infrastructure
{
    public static class ConfigurationServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}