using HM.Core.Messages;
using FluentValidation.Results;
using MediatR;
using HM.Domain.Interfaces;
using FluentValidation;
using HM.API.Services;

namespace HM.API.Application.Commands.Paciente
{
    public class AtualizarPacienteCommandHandler : CommandHandler, IRequestHandler<AtualizarPacienteCommand, ValidationResult>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public AtualizarPacienteCommandHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<ValidationResult> Handle(AtualizarPacienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var actual = await _pacienteRepository.FindAsync(message.Id);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            actual.Update(message.Nome, message.Cpf, message.Email, PasswordHasher.HashPassword(message.Senha));
            _pacienteRepository.Update(actual);

            return await PersistirDados(_pacienteRepository.UnitOfWork);
        }
    }
    public class AtualizarPacienteCommand : Command
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new RegistrarPacienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarPacienteValidation : AbstractValidator<AtualizarPacienteCommand>
        {
            public RegistrarPacienteValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do Paciente inválido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do Paciente não foi informado");
            }

            protected static bool TerEmailValido(string email)
            {
                return Core.DomainObjects.Email.Validar(email);
            }
        }
    }
}