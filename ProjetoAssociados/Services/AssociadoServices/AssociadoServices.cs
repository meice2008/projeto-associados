using Microsoft.EntityFrameworkCore;
using ProjetoAssociados.Data;
using ProjetoAssociados.Models;
using ProjetoAssociados.Services.EmpresaServices;

namespace ProjetoAssociados.Services.AssociadoServices
{
    public class AssociadoServices : IAssociadoServices
    {
        readonly private ApplicationDbContext _context;
        private readonly IEmpresaServices _empresaInterface;
        public AssociadoServices(ApplicationDbContext context, IEmpresaServices empresaInterface)
        {
            _context = context;
            _empresaInterface = empresaInterface;
        }

        public async void DeleteAssociado(int id)
        {
            AssociadoModel Associado = await GetAssociadoById(id);

            try
            {
                _context.Associados.Remove(Associado);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public async Task<AssociadoModel> GetAssociadoById(int? id)
        {
            var associado = _context.Associados.FirstOrDefaultAsync(x => x.Id == id).Result;
            return associado;            
        }

        public async Task<IEnumerable<AssociadoModel>> GetAssociados()
        {
            var associados = _context.Associados;
            return associados;
        }

        public async Task<AssociadoViewModel> Editar(AssociadoViewModel associadoViewModel)
        {
            var associadoSelecionado = GetAssociadoById(associadoViewModel.Id).Result;   
            associadoSelecionado.Nome = associadoViewModel.Nome;
            associadoSelecionado.Cpf = associadoViewModel.Cpf;
            //associadoSelecionado.DtNascimento = associadoViewModel.DtNascimento

            var associadosEmpresa = _empresaInterface.GetEmpresasAssociado().Result;

            foreach (var item in associadosEmpresa)
            {
                if (item.AssociadoId == associadoViewModel.Id)
                {
                    _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }
            }

            foreach (var item in associadoViewModel.Empresas)
            {
                if (item.Checked)
                {
                    _context.AssociadosEmpresa.Add(new AssociadoModelEmpresaModel()
                    {
                        EmpresaId = item.Id,
                        AssociadoId = associadoSelecionado.Id
                    });
                }
            }

            _context.SaveChangesAsync();

            return associadoViewModel;
        }

        public async Task<AssociadoViewModel> GetEditar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                throw new NotImplementedException();
            }

            AssociadoModel associado = GetAssociadoById(Id).Result; 

            if (associado == null)
            {
                throw new NotImplementedException();
            }


            var EmpresasAssociado = from c in _context.Empresas  
                                    select new
                                    {
                                        c.Id,
                                        c.Nome,
                                        Checked = ((from ce in _context.AssociadosEmpresa
                                                    where (ce.AssociadoId == Id) & (ce.EmpresaId == c.Id)
                                                    select ce).Count() > 0)
                                    };

            var associadoViewModel = new AssociadoViewModel();

            associadoViewModel.Id = Id.Value;
            associadoViewModel.Nome = associado.Nome;
            associadoViewModel.Cpf = associado.Cpf;

            var checkboxListAssociados = new List<CheckBoxViewModel>();

            foreach (var item in EmpresasAssociado)
            {
                checkboxListAssociados.Add(new CheckBoxViewModel { Id = item.Id, Nome = item.Nome, Checked = item.Checked });
            }

            associadoViewModel.Empresas = checkboxListAssociados;

            return associadoViewModel;
        }

        public void Cadastrar(AssociadoViewModel associadoViewModel)
        {
            try
            {
                var associado = new AssociadoModel();
                associado.Nome = associadoViewModel.Nome;
                associado.Cpf = associadoViewModel.Cpf;
                associado.Empresas = new List<EmpresaModel>();

                _context.Associados.Add(associado);
                _context.SaveChanges();


                foreach (var item in associadoViewModel.Empresas)
                {
                    if (item.Checked)
                    {
                        _context.AssociadosEmpresa.AddRange(new AssociadoModelEmpresaModel()
                        {
                            AssociadoId = associado.Id, 
                            EmpresaId = item.Id
                        });

                    }
                }

                _context.SaveChanges();

            }
            catch(Exception ex)
            {

            }
        }
    }
}
