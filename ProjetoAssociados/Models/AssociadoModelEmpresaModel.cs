using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoAssociados.Models
{
    [Table("AssociadosEmpresa")]
    public class AssociadoModelEmpresaModel
    {
        [Key]
        public int Id { get; set; }
        public int AssociadoId {get;set;}
        public int EmpresaId { get; set; }

        public virtual EmpresaModel Empresa { get; set; }
        public virtual AssociadoModel Associado { get; set; }

    }
}
