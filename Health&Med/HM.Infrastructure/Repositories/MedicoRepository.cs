using HM.Core.Data;
using HM.Domain.Entities;
using HM.Domain.Interfaces;
using HM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HM.Infrastructure.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly HMDbContext _context;

        public MedicoRepository(HMDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        /// <summary>
        /// Busca e retorna um medico pelo seu registro no banco, recebendo um id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Medico?> FindAsync(Guid id)
        {
            return await _context.Medicos.FindAsync(id);
        }

        /// <summary>
        /// Faz uma busca de todos os médicos
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<Medico>> GetAllAsync()
        {
            return await _context.Medicos.ToListAsync();
        }

        /// <summary>
        /// Registra um medico na base recebendo
        /// TODO: Verificar como criar método de consulta de Medico por e-mail
        /// </summary>
        /// <param name="medico"></param>
        /// <returns></returns>
        public async Task<Medico> AddAsync(Medico medico)
        {
            await _context.AddAsync(medico);
            return medico;
        }

        /// <summary>
        /// Atualiza o medico
        /// </summary>
        /// <param name="medico"></param>
        /// <returns></returns>
        public void Update(Medico medico)
        {
            _context.Medicos.Update(medico);
        }

        /// <summary>
        /// Deleta um medico recebendo o id como parâmetro para encontrar o medico
        /// </summary>
        /// <param name="medico"></param>
        /// <returns></returns>
        public void Delete(Medico medico)
        {
            _context.Medicos.Remove(medico);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
