using ProjetoAssociados.Models;

namespace ProjetoAssociados.Services.AssociadoServices
{
    public interface IAssociadoServices
    {
        Task<IEnumerable<AssociadoModel>> GetAssociados();
        Task<AssociadoModel> GetAssociadoById(int? id);
        void DeleteAssociado(int id);

    }
}
