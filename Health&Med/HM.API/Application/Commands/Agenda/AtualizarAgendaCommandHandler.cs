using FluentValidation.Results;
using HM.Core.Messages;
using HM.Domain.Enum;
using HM.Domain.Interfaces;
using MediatR;

namespace HM.API.Application.Commands.Agenda
{
    public class AtualizarAgendaCommandHandler : CommandHandler, IRequestHandler<AtualizarAgendaCommand, ValidationResult>
    {
        private readonly IAgendaRepository _agendaRepository;

        public AtualizarAgendaCommandHandler(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        public async Task<ValidationResult> Handle(AtualizarAgendaCommand message, CancellationToken cancellationToken)
        {
            var actual = await _agendaRepository.FindAsync(message.AgendaId);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            //Lógica para criar a agenda

            actual.Update(message.MedicoId, message.PacienteId, message.Especialidade, message.DataConsulta, message.InicioConsulta, message.FimConsulta, message.Valor);
            _agendaRepository.Update(actual);

            return await PersistirDados(_agendaRepository.UnitOfWork);
        }
    }

    public class AtualizarAgendaCommand : Command
    {
        public Guid AgendaId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid? PacienteId { get; set; }
        public Especialidade Especialidade { get; set; }
        public DateOnly DataConsulta { get; set; }
        public TimeOnly InicioConsulta { get; set; }
        public TimeOnly FimConsulta { get; set; }
        public Decimal Valor { get; set; }
    }
}