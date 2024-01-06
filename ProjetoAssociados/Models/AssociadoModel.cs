using System.ComponentModel.DataAnnotations;

namespace ProjetoAssociados.Models
{
    public class AssociadoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome Obrigatorio!")]
        [StringLength(200)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Cpf Obrigatorio!")]
        [StringLength(11)]
        public string Cpf { get; set; }
        public DateTime DtNascimento { get; set; } = DateTime.Now;

        public ICollection<EmpresaModel> Empresas { get; set; }
    }
}
