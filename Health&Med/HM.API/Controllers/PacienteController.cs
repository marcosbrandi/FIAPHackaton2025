﻿using HM.API.Application.Commands.Paciente;
using HM.API.Application.Service;
using HM.API.Services;
using HM.Core.Mediator;
using HM.Domain.Dtos;
using HM.Domain.Interfaces;
using HM.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("ListarPacientes")]
        [Authorize]
        public async Task<IActionResult> ListaTodos()
        {
            return CustomResponse(await _pacienteRepository.GetAllAsync());
        }

        [HttpGet("BuscarPacientePorId")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _pacienteRepository.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("CadastrarPaciente")]
        public async Task<IActionResult> Adicionar(NovoPacienteCommand command)
        {
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] PacienteAuthenticateDto request)
        {
            var storedHash = await _pacienteRepository.GetByEmailCpf(request.CpfEmail);
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
                    var token = tokenGenerator.GenerateJwtToken(request.CpfEmail, "Paciente");
                    return Ok(new { Token = token });
                }
            }

            return Unauthorized();
        }

        [HttpPut("AtualizarPaciente")]
        [Authorize (Roles = "Paciente")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarPacienteCommand command)
        {
            var result = await _pacienteRepository.FindAsync(command.PacienteId);
            if (result == null)
            {
                return NotFound();
            }
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpDelete("DeletarPaciente")]
        [Authorize]
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
