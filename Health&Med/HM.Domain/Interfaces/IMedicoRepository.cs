using HM.Core.Data;
using HM.Domain.Entities;

namespace HM.Domain.Interfaces
{
    public interface IMedicoRepository : IRepository<Medico>
    {
        public Task<IEnumerable<Medico>> GetAllAsync();
        public Task<Medico?> FindAsync(Guid id);
        public Task<Medico> AddAsync(Medico medico);
        public void Update(Medico medico);
        public void Delete(Medico medico);
    }
}
