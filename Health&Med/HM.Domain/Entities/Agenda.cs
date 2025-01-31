using HM.Core.DomainObjects;

namespace HM.Domain.Entities
{
    public class Agenda : Entity, IAggregateRoot
    {
        public Agenda(Guid medicoId, Guid? pacienteId, DateTime data)
        {
            MedicoId = medicoId;
            PacienteId = pacienteId;
            Data = data;
        }

        public void Update(Guid medicoId, Guid? pacienteId, DateTime data)
        {
            MedicoId = medicoId;
            PacienteId = pacienteId;
            Data = data;
        }

        public Guid MedicoId { get; private set; }
        public Guid? PacienteId { get; private set; }
        public DateTime Data { get; private set; }

        public Medico? Medico { get; private set; } 
        public Paciente? Paciente { get; private set; }
    }
}