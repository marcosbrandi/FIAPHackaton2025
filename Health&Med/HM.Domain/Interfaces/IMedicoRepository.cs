using HM.Core.Data;
using HM.Domain.Dtos;
using HM.Domain.Entities;
using HM.Domain.Enum;

namespace HM.Domain.Interfaces
{
    public interface IMedicoRepository : IRepository<Medico>
    {
        public Task<IEnumerable<MedicoResponse>> GetAllAsync(string? nome, Especialidade? especialidade);
        public Task<Medico?> GetByCRM(string crm);        
        public Task<Medico?> FindAsync(Guid id);
        public Task<Medico> AddAsync(Medico medico);
        public void Update(Medico medico);
        public void Delete(Medico medico);
    }
}
