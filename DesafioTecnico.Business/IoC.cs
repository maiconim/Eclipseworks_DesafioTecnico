using DesafioTecnico.Business.Commons;
using DesafioTecnico.Business.Repositories;
using DesafioTecnico.Business.Repositories.Impl;
using DesafioTecnico.Business.Services;
using DesafioTecnico.Business.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioTecnico.Business
{
    public static class IoC
    {
        public static IServiceCollection RegistrarIoC(this IServiceCollection services) =>
            services
                .AddDbContext<DesafioTecnicoContext>(o => o.UseSqlite("Data Source=DesafioTecnico.db", x => { x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery); }))
                .RegistrarEntityServices()
                .RegistrarRepositories()
                .RegistrarServices()
                .AddMediatR(o => o.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()))
                ;

        private static IServiceCollection RegistrarEntityServices(this IServiceCollection services) =>
            services;

        private static IServiceCollection RegistrarRepositories(this IServiceCollection services) =>
            services
                .AddScoped<IProjetoRepository, ProjetoRepository>()
                .AddScoped<ITarefaRepository, TarefaRepository>()
                .AddScoped<ITarefaHistoricoRepository, TarefaHistoricoRepository>();

        private static IServiceCollection RegistrarServices(this IServiceCollection services) =>
            services
                .AddScoped<IProjetoService, ProjetoService>()
                .AddScoped<ITarefaService, TarefaService>();
    }
}