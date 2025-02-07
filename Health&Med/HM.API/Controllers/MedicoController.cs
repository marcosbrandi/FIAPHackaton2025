using HM.API.Application.Commands.Medico;
using HM.API.Application.Service;
using HM.API.Services;
using HM.Core.Mediator;
using HM.Domain.Dtos;
using HM.Domain.Enum;
using HM.Domain.Interfaces;
using HM.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM.API.Controllers
{
    public class MedicoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IConfiguration _configuration;
        private readonly IMedicoRepository _medicoRepository;

        public MedicoController(IMediatorHandler mediator, IMedicoRepository medicoRepository, IConfiguration configuration)
        {
            _mediator = mediator;
            _medicoRepository = medicoRepository;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListaTodos([FromQuery] string? nome, [FromQuery] Especialidade? especialidade)
        {
            var result = await _medicoRepository.GetAllAsync(nome, especialidade);
            return CustomResponse(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _medicoRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Adiciona(NovoMedicoCommand command)
        {
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] MedicoAuthenticateDto request)
        {
            var storedHash = await _medicoRepository.GetByCRM(request.Crm);
            if (storedHash == null) return Unauthorized();

            var result = PasswordHasher.VerifyPassword(request.Senha, storedHash.Senha);
            if (result == true)
            {
                string? securityKey = _configuration["Jwt:Key"];
                string? issuer = _configuration["Jwt:Issuer"];
                string? audience = _configuration["Jwt:Audience"];

                if (securityKey != null && issuer != null && audience != null)
                {
                    var tokenGenerator = new JwtTokenService(securityKey, issuer, audience);
                    var token = tokenGenerator.GenerateJwtToken(request.Crm, "Medico");
                    return Ok(new { Token = token });
                }
            }

            return Unauthorized();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, AtualizarMedicoCommand command)
        {
            var result = await _medicoRepository.FindAsync(id);
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
            var result = await _medicoRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return CustomResponse(await _mediator.EnviarComando(new ExcluirMedicoCommand(id)));
        }

    }
}
