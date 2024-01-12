using ProjetoAssociados.Models;

namespace ProjetoAssociados.Services.AssociadoServices
{
    public interface IAssociadoServices
    {
        Task<ServiceResponse<List<AssociadoModel>>> GetAssociados();
        Task<ServiceResponse<AssociadoModel>> GetAssociadoById(int? id);
        void DeleteAssociado(int id);

        Task<AssociadoViewModel> GetEditar(int? Id);
        Task<AssociadoViewModel> Editar(AssociadoViewModel AssociadoViewModel);

        void Cadastrar(AssociadoViewModel associadoViewModel);
    }
}
