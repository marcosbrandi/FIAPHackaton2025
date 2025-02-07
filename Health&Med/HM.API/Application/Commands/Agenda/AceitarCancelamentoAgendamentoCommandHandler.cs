using FluentValidation.Results;
using HM.Core.Messages;
using HM.Domain.Interfaces;
using MediatR;

namespace HM.API.Application.Commands.Agenda
{
    public class AceitarCancelamentoAgendamentoCommandHandler : CommandHandler, IRequestHandler<AceitarCancelamentoAgendamentoCommand, ValidationResult>
    {
        private readonly IAgendaRepository _agendaRepository;

        public AceitarCancelamentoAgendamentoCommandHandler(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        public async Task<ValidationResult> Handle(AceitarCancelamentoAgendamentoCommand message, CancellationToken cancellationToken)
        {
            var actual = await _agendaRepository.FindAsync(message.Id);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            if(message.Aceita)
            {
                actual.AceitarCancelamentoAgendamento();
                _agendaRepository.Update(actual);
                return await PersistirDados(_agendaRepository.UnitOfWork);
            }

            AdicionarErro("Agendamento não Aceito!");
            return ValidationResult;
        }
    }

    public class AceitarCancelamentoAgendamentoCommand : Command
    {
        public Guid Id { get; set; }
        public bool Aceita { get; set; }
    }
}
