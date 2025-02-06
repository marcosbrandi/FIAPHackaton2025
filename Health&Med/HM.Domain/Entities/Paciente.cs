using HM.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace HM.Domain.Entities
{
    public class Paciente : Entity, IAggregateRoot
    {
        public Paciente(string nome, string cpf, string email, string senha)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Senha = senha;
        }

        public void Update(string nome, string cpf, string email, string senha)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Senha = senha;
        }

        [Required(ErrorMessage = "Nome não informado", AllowEmptyStrings = false)]
        [Display(Name = "Nome", Description = "Informe o Nome do Contato.")]
        [StringLength(100, ErrorMessage = "O campo Nome permite até 100 caracteres")]
        public string Nome { get; private set; } = string.Empty;

        public string Cpf { get; private set; } = string.Empty;
        //public string Cpf2
        //{
        //    get { { return Tools.MaskCpf(Cpf) }}

        [Required(ErrorMessage = "e-Mail não informado", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "e-Mail em formato inválido.")]
        [StringLength(200, ErrorMessage = "O campo e-Mail permite até 100 caracteres")]
        public string Email { get; private set; } = string.Empty;
        public string Senha { get; private set; } = string.Empty;

        public ICollection<Agenda>? Agendas { get; private set; }
    }
}
