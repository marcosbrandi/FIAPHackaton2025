using FluentValidation;
using FluentValidation.Results;
using HM.Clientes.API.Application.Events;
using HM.Core.Messages;
using HM.Domain.Enum;
using HM.Domain.Interfaces;
using MediatR;

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
            var cliente = new Domain.Entities.Agenda(message.MedicoId, message.Especialidade, message.DataHora, message.Valor);

            await _agendaRepository.AddAsync(cliente);

            // Adiciona um evento
            //cliente.AdicionarEvento(new ContatoRegistradoEvent(Guid.NewGuid(), message.Nome));

            return await PersistirDados(_agendaRepository.UnitOfWork);
        }
    }

    public class NovoAgendaCommand : Command
    {
        public Guid Id { get; set; }
        public Guid MedicoId { get; set; }
        public Especialidade Especialidade { get; set; }
        public DateTime DataHora { get; set; }
        public Decimal Valor { get; set; }
    }
}