using FluentValidation.Results;
using HM.Core.Messages;
using HM.Domain.Interfaces;
using MediatR;

namespace HM.API.Application.Commands.Agenda
{
    public class AgendarConsultaCommandHandler : CommandHandler, IRequestHandler<AgendarConsultaCommand, ValidationResult>
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;

        public AgendarConsultaCommandHandler(IAgendaRepository agendaRepository, IPacienteRepository pacienteRepository, IMedicoRepository medicoRepository)
        {
            _agendaRepository = agendaRepository;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
        }

        public async Task<ValidationResult> Handle(AgendarConsultaCommand message, CancellationToken cancellationToken)
        {
            // Verifica se o paciente existe
            var paciente = await _pacienteRepository.FindAsync(message.PacienteId);
            if (paciente == null)
            {
                AdicionarErro("Paciente não encontrado!");
                return ValidationResult;
            }

            //Recebe os dados a agenda médica disponível
            var agenda = await _agendaRepository.GetActiveAgenda(message.AgendaId);
            if (agenda == null)
            {
                AdicionarErro("Horário indisponível para o médico selecionado!");
                return ValidationResult;
            }

            agenda.AgendarConsulta(message.PacienteId);
            _agendaRepository.Update(agenda);

            return await PersistirDados(_agendaRepository.UnitOfWork);
        }
    }

    public class AgendarConsultaCommand : Command
    {
        public Guid AgendaId { get; set; }
        public Guid PacienteId { get; set; }
    }
}