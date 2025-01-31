using HM.API.Application.Commands.Medico.Atualizar;
using HM.API.Application.Commands.Medico.Excluir;
using HM.API.Application.Commands.Medico.Novo;
using HM.Core.Mediator;
using HM.Domain.Interfaces;
using HM.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HM.API.Controllers
{
    //[Authorize]
    public class MedicoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IMedicoRepository _medicoRepository;

        public MedicoController(IMediatorHandler mediator, IMedicoRepository medicoRepository)
        {
            _mediator = mediator;
            _medicoRepository = medicoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListaTodos()
        {
            return CustomResponse(await _medicoRepository.GetAllAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var produto = await _medicoRepository.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost("")]
        public async Task<IActionResult> AdicionarPedido(NovoMedicoCommand command)
        {
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPedido(Guid id, AtualizarMedicoCommand command)
        {
            var produto = await _medicoRepository.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await _medicoRepository.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            return CustomResponse(await _mediator.EnviarComando(new ExcluirMedicoCommand(id)));
        }

    }
}
