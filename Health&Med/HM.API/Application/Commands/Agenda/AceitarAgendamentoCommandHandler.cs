using FluentValidation.Results;
using HM.Core.Messages;
using HM.Domain.Interfaces;
using MediatR;

namespace HM.API.Application.Commands.Agenda
{
    public class AceitarAgendamentoCommandHandler : CommandHandler, IRequestHandler<AceitarAgendamentoCommand, ValidationResult>
    {
        private readonly IAgendaRepository _agendaRepository;

        public AceitarAgendamentoCommandHandler(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        public async Task<ValidationResult> Handle(AceitarAgendamentoCommand message, CancellationToken cancellationToken)
        {
            var actual = await _agendaRepository.FindAsync(message.AgendaId);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            actual.AceitarAgendamento(message.Aceita);
            _agendaRepository.Update(actual);

            return await PersistirDados(_agendaRepository.UnitOfWork);
        }
    }

    public class AceitarAgendamentoCommand : Command
    {
        public Guid AgendaId { get; set; }
        public bool Aceita { get; set; }
    }
}
