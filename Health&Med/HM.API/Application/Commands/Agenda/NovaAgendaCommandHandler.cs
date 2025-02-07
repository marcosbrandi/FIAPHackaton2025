using FluentValidation;
using FluentValidation.Results;
using HM.Clientes.API.Application.Events;
using HM.Core.Messages;
using HM.Domain.Enum;
using HM.Domain.Interfaces;
using HM.Infrastructure.Migrations;
using MediatR;
using System;

namespace HM.API.Application.Commands.Agenda
{
    public class NovoAgendaCommandHandler : CommandHandler, IRequestHandler<NovoAgendaCommand, ValidationResult>
    {
        private readonly IAgendaRepository _agendaRepository;

        public NovoAgendaCommandHandler(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        public async Task<ValidationResult> Handle(NovoAgendaCommand message, CancellationToken cancellationToken)
        {
            var somaTempoConsulta = message.TempoMinutosConsulta + message.TempoIntervaloMinutosConsulta;

            var tempoTotalSolicitado = message.FimConsulta - message.InicioConsulta;

            if (somaTempoConsulta < tempoTotalSolicitado.TotalMinutes)
            {
                // Definindo a hora inicial e final
                TimeOnly horaInicial = message.InicioConsulta;
                TimeOnly horaFinal = message.FimConsulta;

                // Intervalo de tempo de consulta (por exemplo, 15 minutos)
                TimeSpan intervaloDeConsulta = new TimeSpan(0, somaTempoConsulta, 0);

                // Loop para somar o intervalo de tempo até a hora final
                TimeOnly currentTime = horaInicial;
                while (currentTime <= horaFinal)
                {
                    Console.WriteLine("Horário de consulta: " + currentTime);

                    //TimeOnly timeOnlyTempoMinutosConsulta = new TimeOnly(message.TempoMinutosConsulta / 60, message.TempoMinutosConsulta % 60);
                    TimeSpan timeSpanTempoMinutosConsulta = TimeSpan.FromMinutes(message.TempoMinutosConsulta);
                    TimeSpan timeOnlyCurrentTime = currentTime.ToTimeSpan();

                    TimeSpan sum = timeOnlyCurrentTime.Add(timeSpanTempoMinutosConsulta);

                    // Converter o resultado de volta para TimeOnly
                    TimeOnly fimConsulta = TimeOnly.FromTimeSpan(sum);

                    await _agendaRepository.AddAsync(
                    new Domain.Entities.Agenda(message.MedicoId, message.Especialidade, message.DataConsulta, currentTime, fimConsulta, message.Valor)
                    );

                    currentTime = currentTime.Add(intervaloDeConsulta);
                }
            }
            else
            {
                await _agendaRepository.AddAsync(
                    new Domain.Entities.Agenda(message.MedicoId, message.Especialidade, message.DataConsulta, message.InicioConsulta, message.FimConsulta, message.Valor)
                    );
            }

            // Adiciona um evento
            //cliente.AdicionarEvento(new ContatoRegistradoEvent(Guid.NewGuid(), message.Nome));

            return await PersistirDados(_agendaRepository.UnitOfWork);
        }
    }

    public class NovoAgendaCommand : Command
    {
        public Guid AgendaId { get; set; }
        public Guid MedicoId { get; set; }
        public Especialidade Especialidade { get; set; }
        public DateOnly DataConsulta { get; set; }
        public TimeOnly InicioConsulta { get; set; }
        public TimeOnly FimConsulta { get; set; }
        public Decimal Valor { get; set; }
        public int TempoMinutosConsulta { get; set; }
        public int TempoIntervaloMinutosConsulta { get; set; }
    }
}