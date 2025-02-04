using HM.Core.Messages;
using FluentValidation;

namespace HM.API.Application.Commands.Medico
{
    public class ExcluirMedicoCommand : Command
    {
        public Guid Id { get; private set; }

        public ExcluirMedicoCommand(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new ExcluirMedicoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ExcluirMedicoValidation : AbstractValidator<ExcluirMedicoCommand>
        {
            public ExcluirMedicoValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do Medico inválido");
            }
        }
    }
}