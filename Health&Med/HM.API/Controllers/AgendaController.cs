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
        [HttpGet]
        public async Task<IActionResult> ListaTodos([FromQuery] Guid? medicoId)
        {
            return CustomResponse(await _agendaRepository.GetAllAsync(medicoId));
        }

        [Authorize]
        [HttpGet("ListaDisponiveis")]
        public async Task<IActionResult> ListaDisponiveis([FromQuery] Guid? medicoId, [FromQuery] DateOnly? dataConsulta)
        {
            return CustomResponse(await _agendaRepository.ListaDisponiveis(medicoId, dataConsulta));
        }

        [Authorize(Roles = "Medico")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _agendaRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Adicionar(NovoAgendaCommand command)
        {
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPut("AgendarConsulta")]
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

        [HttpPatch("CancelarConsulta/{id}")]
        [Authorize(Roles = "Paciente")]
        public async Task<IActionResult> CancelarConsulta([FromRoute] Guid id, CancelarAgendamentoCommand command)
        {
            var result = await _agendaRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, AtualizarAgendaCommand command)
        {
            var result = await _agendaRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPatch("AceitarAgendamento/{id}")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> AceitarAgendamento([FromRoute] Guid id, AceitarAgendamentoCommand command)
        {
            var result = await _agendaRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPatch("AceitarCancelamentoAgendamento/{id}")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> AceitarCancelamentoAgendamento([FromRoute] Guid id, AceitarCancelamentoAgendamentoCommand command)
        {
            var result = await _agendaRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpDelete("{id}")]
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
