﻿using FluentValidation;
using HM.Core.Messages;
using HM.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace HM.API.Application.Commands.Medico
{
    public class NovoMedicoCommand : Command
    {
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public Especialidade Especialidade { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new RegistrarMedicoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarMedicoValidation : AbstractValidator<NovoMedicoCommand>
        {
            public RegistrarMedicoValidation()
            {
                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do Medico não foi informado");
            }

            protected static bool TerEmailValido(string email)
            {
                return Core.DomainObjects.Email.Validar(email);
            }
        }
    }
}