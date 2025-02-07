using FluentValidation.Results;
using HM.Core.Messages;
using HM.Domain.Interfaces;
using MediatR;

namespace HM.API.Application.Commands.Agenda
{
    public class CancelarAgendamentoCommandHandler : CommandHandler, IRequestHandler<CancelarAgendamentoCommand, ValidationResult>
    {
        private readonly IAgendaRepository _agendaRepository;

        public CancelarAgendamentoCommandHandler(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        public async Task<ValidationResult> Handle(CancelarAgendamentoCommand message, CancellationToken cancellationToken)
        {
            var actual = await _agendaRepository.FindAsync(message.AgendaId);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            actual.CancelarAgendamento(message.Justificativa);
            _agendaRepository.Update(actual);

            return await PersistirDados(_agendaRepository.UnitOfWork);
        }
    }

    public class CancelarAgendamentoCommand : Command
    {
        public Guid AgendaId { get; set; }
        public string Justificativa { get; set; }
    }
}
