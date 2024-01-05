using System.ComponentModel.DataAnnotations;

namespace ProjetoAssociados.Models
{
    public class AssociadoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome Obrigatorio!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Cpf Obrigatorio!")]
        public string Cpf { get; set; }
        public DateTime DtNascimento { get; set; } = DateTime.Now;
    }
}
