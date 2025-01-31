using FluentValidation.Results;
using HM.API.Application.Commands.Medico.Atualizar;
using HM.API.Application.Commands.Medico.Novo;
using HM.Core.Mediator;
using HM.Domain.Interfaces;
using HM.Infrastructure.Data;
using HM.Infrastructure.Repositories;
using MediatR;

namespace HM.Clientes.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IRequestHandler<NovoMedicoCommand, ValidationResult>, NovoMedicoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarMedicoCommand, ValidationResult>, AtualizarMedicoCommandHandler>();

            //services.AddScoped<IRequestHandler<NovoPacienteCommand, ValidationResult>, NovoPacienteCommandHandler>();
            //services.AddScoped<IRequestHandler<AtualizarPacienteCommand, ValidationResult>, AtualizarPacienteCommandHandler>();

            //services.AddScoped<INotificationHandler<MedicoRegistradoEvent>, MedicoEventHandler>();

            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<HMDbContext>();
        }
    }
}