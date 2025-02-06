using HM.Core.Data;
using HM.Domain.Entities;

namespace HM.Domain.Interfaces
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        public Task<IEnumerable<Paciente>> GetAllAsync();
        public Task<Paciente?> FindAsync(Guid id);
        public Task<Paciente?> GetByEmailCpf(string emailCpf);
        public Task<Paciente?> Authenticate(string emailCpf, string senha);
        public Task<Paciente> AddAsync(Paciente paciente);
        public void Update(Paciente paciente);
        public void Delete(Paciente paciente);
    }
}
