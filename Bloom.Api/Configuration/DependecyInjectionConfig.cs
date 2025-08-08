using Bloom.Data.DbContext;
using Bloom.Data.Repository;
using Bloom.Negocio.Interfaces;
using Bloom.Negocio.Notificacoes;
using Bloom.Negocio.Services;
using System.Collections.Generic;

namespace Bloom.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            services.AddScoped<ApiDBContext>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
