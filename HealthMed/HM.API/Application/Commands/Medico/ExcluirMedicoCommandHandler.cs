using HM.Core.Messages;
using FluentValidation.Results;
using MediatR;
using HM.Domain.Interfaces;

namespace HM.API.Application.Commands.Medico
{
    public class ExcluirMedicoCommandHandler : CommandHandler, IRequestHandler<ExcluirMedicoCommand, ValidationResult>
    {
        private readonly IMedicoRepository _medicoRepository;

        public ExcluirMedicoCommandHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<ValidationResult> Handle(ExcluirMedicoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var actual = await _medicoRepository.FindAsync(message.Id);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            _medicoRepository.Delete(actual);

            return await PersistirDados(_medicoRepository.UnitOfWork);
        }
    }
}