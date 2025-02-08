using HM.Core.Data;
using HM.Domain.Entities;

namespace HM.Domain.Interfaces
{
    public interface IAgendaRepository : IRepository<Agenda>
    {
        public Task<IEnumerable<Agenda>> GetAllAsync(Guid? medicoId);
        public Task<IEnumerable<Agenda>> ListaDisponiveis(Guid? medicoId, DateOnly? dataConsulta);
        public Task<Agenda?> FindAsync(Guid id);
        public Task<Agenda?> GetActiveAgenda(Guid id);
        public Task<Agenda> AddAsync(Agenda agenda);
        public void Update(Agenda agenda);
        public void Delete(Agenda agenda);
    }
}
