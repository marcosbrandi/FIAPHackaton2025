using HM.Core.Data;
using HM.Domain.Dtos;
using HM.Domain.Entities;

namespace HM.Domain.Interfaces
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        public Task<IEnumerable<PacienteResponse>> GetAllAsync();
        public Task<Paciente?> FindAsync(Guid id);
        public Task<Paciente?> GetByEmailCpf(string emailCpf);
        public Task<Paciente> AddAsync(Paciente paciente);
        public void Update(Paciente paciente);
        public void Delete(Paciente paciente);
    }
}
