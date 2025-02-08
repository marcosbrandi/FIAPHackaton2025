using FluentValidation.Results;
using HM.Core.Messages;
using HM.Domain.Interfaces;
using MediatR;

namespace HM.API.Application.Commands.Agenda
{
    public class AgendarConsultaCommandHandler : CommandHandler, IRequestHandler<AgendarConsultaCommand, ValidationResult>
    {
        private readonly IAgendaRepository _agendaRepository;

        public AgendarConsultaCommandHandler(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        public async Task<ValidationResult> Handle(AgendarConsultaCommand message, CancellationToken cancellationToken)
        {
            //// Verifica se o paciente existe
            //var paciente = await _context.Pacientes.FindAsync(consultaDTO.PacienteId);
            //if (paciente == null)
            //{
            //    return BadRequest("Paciente não encontrado.");
            //}

            //// Verifica se o médico existe
            //var medico = await _context.Medicos.FindAsync(consultaDTO.MedicoId);
            //if (medico == null)
            //{
            //    return BadRequest("Médico não encontrado.");
            //}

            //// Verifica se o horário está disponível na agenda do médico
            //var horarioDisponivel = await _context.Agendas
            //    .AnyAsync(a => a.MedicoId == consultaDTO.MedicoId && a.DataHora == consultaDTO.DataHora);

            //if (!horarioDisponivel)
            //{
            //    return BadRequest("Horário indisponível para o médico selecionado.");
            //}

            //// Verifica se já existe uma consulta agendada no mesmo horário para o médico
            //var consultaExistente = await _context.Consultas
            //    .AnyAsync(c => c.MedicoId == consultaDTO.MedicoId && c.DataHora == consultaDTO.DataHora);

            //if (consultaExistente)
            //{
            //    return BadRequest("Já existe uma consulta agendada para este horário.");
            //}

            //// Cria uma nova consulta a partir do DTO
            //var consulta = new Consulta
            //{
            //    DataHora = consultaDTO.DataHora,
            //    PacienteId = consultaDTO.PacienteId,
            //    MedicoId = consultaDTO.MedicoId
            //};

            //// Adiciona a consulta ao banco de dados
            //_context.Consultas.Add(consulta);
            //await _context.SaveChangesAsync();

            //return Ok(new { Message = "Consulta agendada com sucesso!", Consulta = consulta });

            var actual = await _agendaRepository.FindAsync(message.Id);

            if (actual == null)
            {
                AdicionarErro("Objeto não localizado!");
                return ValidationResult;
            }

            actual.AgendarConsulta(message.PacienteId);
            _agendaRepository.Update(actual);

            return await PersistirDados(_agendaRepository.UnitOfWork);
        }
    }

    public class AgendarConsultaCommand : Command
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }
    }
}