using ProjetoAssociados.Models;

namespace ProjetoAssociados.Services.AssociadoServices
{
    public interface IAssociadoServices
    {
        Task<IEnumerable<AssociadoModel>> GetAssociados();
        Task<AssociadoModel> GetAssociadoById(int? id);
        void DeleteAssociado(int id);

        Task<AssociadoViewModel> GetEditar(int? Id);
        Task<AssociadoViewModel> Editar(AssociadoViewModel AssociadoViewModel);

        void Cadastrar(AssociadoViewModel associadoViewModel);
    }
}
