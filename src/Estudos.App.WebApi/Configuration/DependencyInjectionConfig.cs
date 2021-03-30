using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Notificacoes;
using Estudos.App.Business.Services;
using Estudos.App.Data.Context;
using Estudos.App.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Estudos.App.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            
            return services;
        }
    }
}