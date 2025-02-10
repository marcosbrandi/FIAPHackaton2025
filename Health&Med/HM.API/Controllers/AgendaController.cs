using HM.API.Application.Commands.Agenda;
using HM.Core.Mediator;
using HM.Domain.Interfaces;
using HM.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM.API.Controllers
{
    public class AgendaController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAgendaRepository _agendaRepository;

        public AgendaController(IMediatorHandler mediator, IAgendaRepository agendaRepository)
        {
            _mediator = mediator;
            _agendaRepository = agendaRepository;
        }
        
        [Authorize(Roles = "Medico")]
        [HttpGet("ListarAgendas")]
        public async Task<IActionResult> ListaTodos([FromQuery] Guid? medicoId)
        {
            return CustomResponse(await _agendaRepository.GetAllAsync(medicoId));
        }

        [Authorize]
        [HttpGet("ListarAgendasDisponiveis")]
        public async Task<IActionResult> ListaDisponiveis([FromQuery] Guid? medicoId, [FromQuery] DateOnly? dataConsulta)
        {
            return CustomResponse(await _agendaRepository.ListaDisponiveis(medicoId, dataConsulta));
        }

        [Authorize(Roles = "Medico")]
        [HttpGet("BuscarAgendaMedico")]
        public async Task<IActionResult> Get([FromQuery] Guid agendaId)
        {
            var result = await _agendaRepository.FindAsync(agendaId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("CadatrarAgendaMedico")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Adicionar(NovoAgendaCommand command)
        {
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPut("AgendarConsultaPaciente")]
        [Authorize(Roles = "Paciente")]
        public async Task<IActionResult> AgendarConsulta([FromBody] AgendarConsultaCommand command)
        {
            var result = await _agendaRepository.FindAsync(command.AgendaId);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPatch("CancelarConsultaPaciente")]
        [Authorize(Roles = "Paciente")]
        public async Task<IActionResult> CancelarConsulta([FromBody] CancelarAgendamentoCommand command)
        {
            var result = await _agendaRepository.FindAsync(command.AgendaId);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPut("AtualizarAgendaMedico")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarAgendaCommand command)
        {
            var result = await _agendaRepository.FindAsync(command.AgendaId);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPatch("AceitarAgendamentoMedico")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> AceitarAgendamento([FromBody] AceitarAgendamentoCommand command)
        {
            var result = await _agendaRepository.FindAsync(command.AgendaId);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPatch("AceitarCancelamentoAgendamentoMedico")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> AceitarCancelamentoAgendamento([FromBody] AceitarCancelamentoAgendamentoCommand command)
        {
            var result = await _agendaRepository.FindAsync(command.AgendaId);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpDelete("DeletarAgenda")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _agendaRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return CustomResponse(await _mediator.EnviarComando(new ExcluirAgendaCommand(id)));
        }
    }
}
