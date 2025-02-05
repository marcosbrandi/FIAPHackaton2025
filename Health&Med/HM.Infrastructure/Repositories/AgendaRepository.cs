using HM.Core.Data;
using HM.Domain.Entities;
using HM.Domain.Enum;
using HM.Domain.Interfaces;
using HM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HM.Infrastructure.Repositories
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly HMDbContext _context;

        public AgendaRepository(HMDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        /// <summary>
        /// Busca e retorna um agenda pelo seu registro no banco, recebendo um id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Agenda?> FindAsync(Guid id)
        {
            return await _context.Agendas.FindAsync(id);
        }

        /// <summary>
        /// Busca e retorna um agenda ativa pelo seu registro no banco, recebendo um id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Agenda?> GetActiveAgenda(Guid id)
        {
            return await _context.Agendas.FirstOrDefaultAsync(x => x.Id == id && x.PacienteId == null);
        }

        /// <summary>
        /// Faz uma busca de todos os médicos
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<Agenda>> GetAllAsync(Guid? medicoId)
        {
            var result = _context.Agendas.AsNoTracking();

            if (medicoId != null)
            {
                result = result.Where(x => x.MedicoId == medicoId);
            }

            return await result.ToListAsync();
        }

        /// <summary>
        /// Registra um agenda na base recebendo
        /// TODO: Verificar como criar método de consulta de Agenda por e-mail
        /// </summary>
        /// <param name="agenda"></param>
        /// <returns></returns>
        public async Task<Agenda> AddAsync(Agenda agenda)
        {
            await _context.AddAsync(agenda);
            return agenda;
        }

        /// <summary>
        /// Atualiza o agenda
        /// </summary>
        /// <param name="agenda"></param>
        /// <returns></returns>
        public void Update(Agenda agenda)
        {
            _context.Agendas.Update(agenda);
        }

        /// <summary>
        /// Deleta um agenda recebendo o id como parâmetro para encontrar o agenda
        /// </summary>
        /// <param name="agenda"></param>
        /// <returns></returns>
        public void Delete(Agenda agenda)
        {
            _context.Agendas.Remove(agenda);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

