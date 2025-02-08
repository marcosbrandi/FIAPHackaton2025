using HM.Core.Messages;
using FluentValidation.Results;
using MediatR;
using HM.Domain.Interfaces;
using FluentValidation;

namespace HM.API.Application.Commands.Paciente
{
    public class ExcluirPacienteCommandHandler : CommandHandler, IRequestHandler<ExcluirPacienteCommand, ValidationResult>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public ExcluirPacienteCommandHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<ValidationResult> Handle(ExcluirPacienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var actual = await _pacienteRepository.FindAsync(message.Id);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            _pacienteRepository.Delete(actual);

            return await PersistirDados(_pacienteRepository.UnitOfWork);
        }
    }
    public class ExcluirPacienteCommand : Command
    {
        public Guid Id { get; private set; }

        public ExcluirPacienteCommand(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new ExcluirPacienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ExcluirPacienteValidation : AbstractValidator<ExcluirPacienteCommand>
        {
            public ExcluirPacienteValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do Paciente inválido");
            }
        }
    }
}