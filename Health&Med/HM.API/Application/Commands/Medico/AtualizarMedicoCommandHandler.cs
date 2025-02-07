using HM.Core.Messages;
using FluentValidation.Results;
using MediatR;
using HM.Domain.Interfaces;
using HM.API.Services;

namespace HM.API.Application.Commands.Medico
{
    public class AtualizarMedicoCommandHandler : CommandHandler, IRequestHandler<AtualizarMedicoCommand, ValidationResult>
    {
        private readonly IMedicoRepository _medicoRepository;

        public AtualizarMedicoCommandHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<ValidationResult> Handle(AtualizarMedicoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var actual = await _medicoRepository.FindAsync(message.Id);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            actual.Update(message.Nome, message.Cpf, message.Crm, message.Especialidade, message.Email, PasswordHasher.HashPassword(message.Senha));
            _medicoRepository.Update(actual);

            return await PersistirDados(_medicoRepository.UnitOfWork);
        }
    }
}