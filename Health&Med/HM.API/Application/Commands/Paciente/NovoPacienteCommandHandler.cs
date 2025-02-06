using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using HM.API.Services;
using HM.Clientes.API.Application.Events;
using HM.Core.Messages;
using HM.Domain.Interfaces;
using MediatR;

namespace HM.API.Application.Commands.Paciente
{
    public class NovoPacienteCommandHandler : CommandHandler, IRequestHandler<NovoPacienteCommand, ValidationResult>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public NovoPacienteCommandHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<ValidationResult> Handle(NovoPacienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = new Domain.Entities.Paciente(message.Nome, message.Cpf, message.Email, PasswordHasher.HashPassword(message.Senha));

            await _pacienteRepository.AddAsync(cliente);

            // Quando precisar adicionar algum evento
            cliente.AdicionarEvento(new ContatoRegistradoEvent(Guid.NewGuid(), message.Nome));

            return await PersistirDados(_pacienteRepository.UnitOfWork);
        }
    }
    public class NovoPacienteCommand : Command
    {
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new RegistrarPacienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarPacienteValidation : AbstractValidator<NovoPacienteCommand>
        {
            public RegistrarPacienteValidation()
            {
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