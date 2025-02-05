using HM.API.Application.Commands.Agenda;
using HM.Core.Mediator;
using HM.Domain.Interfaces;
using HM.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM.API.Controllers
{
    //[Authorize]
    public class AgendaController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAgendaRepository _agendaRepository;

        public AgendaController(IMediatorHandler mediator, IAgendaRepository agendaRepository)
        {
            _mediator = mediator;
            _agendaRepository = agendaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListaTodos([FromQuery] Guid? medicoId)
        {
            return CustomResponse(await _agendaRepository.GetAllAsync(medicoId));
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
        //[Authorize(Roles = "Medico")]
        public async Task<IActionResult> Adicionar(NovoAgendaCommand command)
        {
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPost("AgendarConsulta")]
        //[Authorize(Roles = "Paciente")]
        public async Task<IActionResult> AgendarConsulta([FromBody] AgendarConsultaCommand command)
        {
            var result = await _agendaRepository.FindAsync(command.Id);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Medico")]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, AtualizarAgendaCommand command)
        {
            var result = await _agendaRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPatch("{id}")]
        //[Authorize(Roles = "Medico")]
        public async Task<IActionResult> AceitarAgendamento([FromRoute] Guid id, AceitarAgendamentoCommand command)
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
