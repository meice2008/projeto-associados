using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAssociados.Data;
using ProjetoAssociados.Models;

namespace ProjetoAssociados.Services.EmpresaServices
{
    public class EmpresaServices : IEmpresaServices
    {
        readonly private ApplicationDbContext _context;
        public EmpresaServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void DeleteEmpresa(int id)
        {
            EmpresaModel empresa = await GetEmpresaById(id);

            try
            {
                _context.Empresas.Remove(empresa);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<EmpresaModel> GetEmpresaById(int? id)
        {
            var empresa = _context.Empresas.FirstOrDefaultAsync(x => x.Id == id).Result;
            return empresa;
        }

        public async Task<IEnumerable<EmpresaModel>> GetEmpresas()
        {
            var empresas = _context.Empresas;
            return empresas;

        }

        public async Task<IEnumerable<AssociadoModelEmpresaModel>> GetEmpresasAssociado()
        {
            var associadosEmpresa = _context.AssociadosEmpresa;
            return associadosEmpresa;
        }

        public async Task<EmpresaViewModel> GetEditar(int? Id)
        {

            if (Id == null || Id == 0)
            {
                throw new NotImplementedException();
            }

            EmpresaModel empresaModel = GetEmpresaById(Id).Result;

            if (empresaModel == null)
            {
                throw new NotImplementedException();
            }

            var AssociadosEmpresa = from c in _context.Associados 
                                    select new
                                    {
                                        c.Id,
                                        c.Nome,
                                        Checked = ((from ce in _context.AssociadosEmpresa
                                                    where (ce.EmpresaId == Id) & (ce.AssociadoId == c.Id)
                                                    select ce).Count() > 0)
                                    };

            EmpresaViewModel empresaViewModel = new EmpresaViewModel();

            empresaViewModel.Id = Id.Value;
            empresaViewModel.Nome = empresaModel.Nome;
            empresaViewModel.Cnpj = empresaModel.Cnpj;

            var checkboxListAssociados = new List<CheckBoxViewModel>();

            foreach (var item in AssociadosEmpresa)
            {
                checkboxListAssociados.Add(new CheckBoxViewModel { Id = item.Id, Nome = item.Nome, Checked = item.Checked });
            }

            empresaViewModel.Associados = checkboxListAssociados;

            return empresaViewModel;

        }

        public async Task<EmpresaViewModel> Editar(EmpresaViewModel empresaViewModel)
        {
            var empresaSelecionada = GetEmpresaById(empresaViewModel.Id).Result; 
            empresaSelecionada.Nome = empresaViewModel.Nome;
            empresaSelecionada.Cnpj = empresaViewModel.Cnpj;

            var associadosEmpresa = GetEmpresasAssociado().Result;

            foreach (var item in associadosEmpresa)
            {
                if (item.EmpresaId == empresaViewModel.Id)
                {
                    _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }
            }

            foreach (var item in empresaViewModel.Associados)
            {
                if (item.Checked)
                {
                    _context.AssociadosEmpresa.Add(new AssociadoModelEmpresaModel()
                    {
                        EmpresaId = empresaViewModel.Id,
                        AssociadoId = item.Id
                    });
                }
            }

            _context.SaveChangesAsync();

            return empresaViewModel;

        }

        public void Cadastrar(EmpresaViewModel empresaViewModel)
        {
            try
            {
                var empresa = new EmpresaModel();
                empresa.Nome = empresaViewModel.Nome;
                empresa.Cnpj = empresaViewModel.Cnpj;
                empresa.Associados = new List<AssociadoModel>();

                _context.Empresas.Add(empresa);
                _context.SaveChanges();


                foreach (var item in empresaViewModel.Associados)
                {
                    if (item.Checked)
                    {
                        _context.AssociadosEmpresa.AddRange(new AssociadoModelEmpresaModel()
                        {
                            EmpresaId = empresa.Id, 
                            AssociadoId = item.Id
                        });

                    }
                }

                _context.SaveChanges();


            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
