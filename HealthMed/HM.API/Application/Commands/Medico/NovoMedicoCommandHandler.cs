using FluentValidation.Results;
using HM.Clientes.API.Application.Events;
using HM.Core.Messages;
using HM.Domain.Interfaces;
using MediatR;

namespace HM.API.Application.Commands.Medico
{
    public class NovoMedicoCommandHandler : CommandHandler, IRequestHandler<NovoMedicoCommand, ValidationResult>
    {
        private readonly IMedicoRepository _medicoRepository;

        public NovoMedicoCommandHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<ValidationResult> Handle(NovoMedicoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = new Domain.Entities.Medico(message.Nome, message.Cpf, message.Crm, message.Especialidade, message.Email, message.Senha);

            await _medicoRepository.AddAsync(cliente);

            // Quando precisar adicionar algum evento
            cliente.AdicionarEvento(new ContatoRegistradoEvent(Guid.NewGuid(), message.Nome));

            return await PersistirDados(_medicoRepository.UnitOfWork);
        }
    }
}