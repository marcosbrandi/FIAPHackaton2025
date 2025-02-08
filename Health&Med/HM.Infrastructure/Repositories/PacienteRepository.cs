using HM.Core.Data;
using HM.Domain.Dtos;
using HM.Domain.Entities;
using HM.Domain.Interfaces;
using HM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HM.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly HMDbContext _context;

        public PacienteRepository(HMDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        /// <summary>
        /// Busca e retorna um paciente pelo seu registro no banco, recebendo um id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Paciente?> FindAsync(Guid id)
        {
            return await _context.Pacientes.FindAsync(id);
        }
        public async Task<Paciente?> GetByEmailCpf(string emailCpf)
        {
            return await _context.Pacientes.FirstOrDefaultAsync(x => x.Email == emailCpf || x.Cpf == emailCpf);
        }

        /// <summary>
        /// Faz uma busca de todos os médicos
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<PacienteResponse>> GetAllAsync()
        {
            var res = await _context.Pacientes.ToListAsync();
            return res.Select(x=> new PacienteResponse(Id: x.Id, Nome: x.Nome, Cpf: x.DisplayCpf, Email: x.Email));
        }

        /// <summary>
        /// Registra um paciente na base recebendo
        /// TODO: Verificar como criar método de consulta de Paciente por e-mail
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public async Task<Paciente> AddAsync(Paciente paciente)
        {
            await _context.AddAsync(paciente);
            return paciente;
        }

        /// <summary>
        /// Atualiza o paciente
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public void Update(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
        }

        /// <summary>
        /// Deleta um paciente recebendo o id como parâmetro para encontrar o paciente
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public void Delete(Paciente paciente)
        {
            _context.Pacientes.Remove(paciente);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
