using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAssociados.Models
{
    public class EmpresaModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Nome { get; set; }
        
        [StringLength(14)]
        public string Cnpj { get; set; }

        public ICollection<AssociadoModel>? Associados { get; set; }
    }

}
