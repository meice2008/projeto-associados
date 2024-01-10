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
            ////
            //var client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync("https://localhost:7063/api/Associado/" + id);
            ////response.EnsureSuccessStatusCode();
            //string res = await response.Content.ReadAsStringAsync();
            //var final = JsonConvert.DeserializeObject<ServiceResponse<AssociadoModel>>(res);
            ////

            //return final.Dados;

            var associado = _context.Associados.FirstOrDefaultAsync(x => x.Id == id).Result;
            return associado;            
        }

        public async Task<IEnumerable<AssociadoModel>> GetAssociados()
        {

            //
            //var client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync("https://localhost:7063/api/Associado");
            ////response.EnsureSuccessStatusCode();
            //string res = await response.Content.ReadAsStringAsync();
            //var final = JsonConvert.DeserializeObject<ServiceResponse<List<AssociadoModel>>>(res);
            ////

            //return final.Dados;

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

            CadastrarSociedade(associadoSelecionado.Id, associadoViewModel.Empresas);

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

            var EmpresasAssociado = GetEmpresasAssociadas(associado.Id);

            var associadoViewModel = new AssociadoViewModel()
            {
                Id = Id.Value,
                Nome = associado.Nome,
                Cpf = associado.Cpf
            };

            var checkboxListAssociados = new List<CheckBoxViewModel>();

            foreach (var item in EmpresasAssociado)
            {
                checkboxListAssociados.Add(new CheckBoxViewModel { Id = item.Id, Nome = item.Nome, Checked = item.Checked });
            }

            associadoViewModel.Empresas = checkboxListAssociados;

            return associadoViewModel;
        }

        public async void Cadastrar(AssociadoViewModel associadoViewModel)
        {

            try
            {                

                var associado = new AssociadoModel()
                {
                    Nome = associadoViewModel.Nome,
                    Cpf = associadoViewModel.Cpf
                };                

                _context.Associados.Add(associado);
                _context.SaveChanges();

                CadastrarSociedade(associado.Id, associadoViewModel.Empresas);
                
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public void CadastrarSociedade(int IdAssociado, List<CheckBoxViewModel> sociedade)
        {

            try
            {

                foreach (var item in sociedade)
                {

                    if (item.Checked)
                    {

                        var associar = new AssociadoModelEmpresaModel()
                        {
                            AssociadoId = IdAssociado,
                            EmpresaId = item.Id
                        };

                        _context.AssociadosEmpresa.AddRange(associar);

                    }
                }

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CheckBoxViewModel> GetEmpresasAssociadas(int IdAssociado)
        {
            var lstEmpresas = new List<CheckBoxViewModel>();

            try
            {                

                var EmpresasAssociado = from c in _context.Empresas
                                        select new CheckBoxViewModel
                                        {
                                            Id = c.Id,
                                            Nome = c.Nome,
                                            Checked = _context.AssociadosEmpresa
                                                            .Any(ce => ce.AssociadoId == IdAssociado && ce.EmpresaId == c.Id)
                                        };

                lstEmpresas = EmpresasAssociado.ToList();

            }
            catch(Exception ex)
            {
                throw ex;
            }


            return lstEmpresas;
        }

    }
}
