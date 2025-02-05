using HM.Core.DomainObjects;
using HM.Domain.Enum;

namespace HM.Domain.Entities
{
    public class Agenda : Entity, IAggregateRoot
    {
        public Agenda(Guid medicoId, Especialidade especialidade, DateOnly dataConsulta, TimeOnly inicioConsulta, TimeOnly fimConsulta, decimal valor)
        {
            MedicoId = medicoId;
            Especialidade = especialidade;
            DataConsulta = dataConsulta;
            InicioConsulta = inicioConsulta;
            FimConsulta = fimConsulta;
            Valor = valor;
        }

        public void Update(Guid medicoId, Guid? pacienteId, Especialidade especialidade, DateOnly dataConsulta, TimeOnly inicioConsulta, TimeOnly fimConsulta, decimal valor)
        {
            MedicoId = medicoId;
            PacienteId = pacienteId;
            Especialidade = especialidade;
            DataConsulta = dataConsulta;
            InicioConsulta = inicioConsulta;
            FimConsulta = fimConsulta;
            Valor = valor;
        }

        public void AgendarConsulta(Guid pacienteId)
        {
            PacienteId = pacienteId;
        }

        public void AceitarAgendamento(bool aceita)
        {
            Aceita = aceita;
        }

        public Guid MedicoId { get; private set; }
        public Guid? PacienteId { get; private set; }
        public Especialidade Especialidade { get; private set; }
        public DateOnly DataConsulta { get; private set; }
        public TimeOnly InicioConsulta { get; private set; }
        public TimeOnly FimConsulta { get; private set; }
        public Decimal Valor { get; private set; }        
        public bool Aceita { get; private set; }

        public Medico? Medico { get; private set; }
        public Paciente? Paciente { get; private set; }
    }
}