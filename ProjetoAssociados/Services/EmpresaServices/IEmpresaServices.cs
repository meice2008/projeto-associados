using ProjetoAssociados.Models;

namespace ProjetoAssociados.Services.EmpresaServices
{
    public interface IEmpresaServices
    {
        Task<IEnumerable<EmpresaModel>> GetEmpresas();
        Task<EmpresaModel> GetEmpresaById(int? id);
        void DeleteEmpresa(int id);
        Task<IEnumerable<AssociadoModelEmpresaModel>> GetEmpresasAssociado();
        Task<EmpresaViewModel> GetEditar(int? Id);
        Task<EmpresaViewModel> Editar(EmpresaViewModel empresaViewModel);
        void Cadastrar(EmpresaViewModel empresaViewModel);
    }
}
