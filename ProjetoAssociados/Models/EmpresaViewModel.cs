using System.ComponentModel.DataAnnotations;

namespace ProjetoAssociados.Models
{
    public class EmpresaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public List<CheckBoxViewModel>? Associados { get; set; }
    }
}
