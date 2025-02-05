﻿using HM.API.Application.Commands.Paciente;
using HM.API.Application.Dtos;
using HM.API.Application.Service;
using HM.Core.Mediator;
using HM.Domain.Interfaces;
using HM.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Dtos;

namespace HM.API.Controllers
{
    public class PacienteController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IConfiguration _configuration;
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteController(IMediatorHandler mediator, IPacienteRepository pacienteRepository, IConfiguration configuration)
        {
            _mediator = mediator;
            _pacienteRepository = pacienteRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> ListaTodos()
        {
            return CustomResponse(await _pacienteRepository.GetAllAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _pacienteRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> Adicionar(NovoPacienteCommand command)
        {
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] PacienteAuthenticateDto request)
        {
            var paciente = await _pacienteRepository.Authenticate(request.CpfEmail, request.Senha);
            if (paciente == null) return Unauthorized();

            string? securityKey = _configuration["Jwt:Key"];
            string? issuer = _configuration["Jwt:Issuer"];
            string? audience = _configuration["Jwt:Audience"];

            if (securityKey != null && issuer != null && audience != null)
            {
                var tokenGenerator = new JwtTokenService(securityKey, issuer, audience);
                var token = tokenGenerator.GenerateJwtToken(request.CpfEmail, "Paciente");
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, AtualizarPacienteCommand command)
        {
            var result = await _pacienteRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _pacienteRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return CustomResponse(await _mediator.EnviarComando(new ExcluirPacienteCommand(id)));
        }

    }
}
