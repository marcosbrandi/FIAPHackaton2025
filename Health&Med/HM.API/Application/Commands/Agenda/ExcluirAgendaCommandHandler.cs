using FluentValidation.Results;
using HM.Core.Messages;
using HM.Domain.Interfaces;
using MediatR;

namespace HM.API.Application.Commands.Agenda
{
    public class ExcluirAgendaCommandHandler : CommandHandler, IRequestHandler<ExcluirAgendaCommand, ValidationResult>
    {
        private readonly IAgendaRepository _agendaRepository;

        public ExcluirAgendaCommandHandler(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        public async Task<ValidationResult> Handle(ExcluirAgendaCommand message, CancellationToken cancellationToken)
        {
            var actual = await _agendaRepository.FindAsync(message.Id);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            _agendaRepository.Delete(actual);

            return await PersistirDados(_agendaRepository.UnitOfWork);
        }
    }

    public class ExcluirAgendaCommand : Command
    {
        public Guid Id { get; set; }

        public ExcluirAgendaCommand(Guid id)
        {
            Id = id;
        }
    }
}