using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjetoAssociados.Data;
using ProjetoAssociados.Models;
using System.Text;

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

            const string apiUrl = "https://localhost:7063/api/Empresa/";

            EmpresaModel empresa = await GetEmpresaById(id);

            try
            {
                

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl+ empresa.Id);
                        response.EnsureSuccessStatusCode();

                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<List<EmpresaModel>>>(jsonResponse);

                        //return serviceResponse.Dados;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                //_context.Empresas.Remove(empresa);
                //_context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<EmpresaModel> GetEmpresaById(int? id)
        {

            const string apiUrl = "https://localhost:7063/api/Empresa/";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl+id);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<EmpresaModel>>(jsonResponse);

                    return serviceResponse.Dados;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //
            //var client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync("https://localhost:7063/api/Empresa/"+id);
            ////response.EnsureSuccessStatusCode();
            //string res = await response.Content.ReadAsStringAsync();
            //var final = JsonConvert.DeserializeObject<ServiceResponse<EmpresaModel>>(res);
            ////

            //return final.Dados;

            //var empresa = _context.Empresas.FirstOrDefaultAsync(x => x.Id == id).Result;
            //return empresa;
        }

        public async Task<IEnumerable<EmpresaModel>> GetEmpresas()
        {
            const string apiUrl = "https://localhost:7063/api/Empresa";


            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<List<EmpresaModel>>>(jsonResponse);

                    return serviceResponse.Dados;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
            //var empresas = _context.Empresas;
            //return empresas;

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

            var AssociadosEmpresa = GetAssociadosEmpresa(empresaModel.Id);

            EmpresaViewModel empresaViewModel = new EmpresaViewModel()
            {
                Id = Id.Value,
                Nome = empresaModel.Nome,
                Cnpj = empresaModel.Cnpj
            };

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

            CadastrarSociedade(empresaViewModel.Id, empresaViewModel.Associados);            

            return empresaViewModel;

        }

        public async void Cadastrar(EmpresaViewModel empresaViewModel)
        {
            const string apiUrl = "https://localhost:7063/api/Empresa";

            try
            {                                

                using (var httpClient = new HttpClient())
                {
                    try
                    {

                        string empresaJson = JsonConvert.SerializeObject(empresaViewModel);

                        HttpContent content = new StringContent(empresaJson, Encoding.UTF8, "application/json");                        

                        HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
                        response.EnsureSuccessStatusCode();

                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<List<EmpresaModel>>>(jsonResponse);

                        //return serviceResponse.Dados;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                
                //_context.Empresas.Add(empresa);
                //_context.SaveChanges();

                //CadastrarSociedade(empresa.Id, empresaViewModel.Associados);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CadastrarSociedade(int IdEmpresa, List<CheckBoxViewModel> sociedade)
        {

            try
            {

                foreach (var item in sociedade)
                {

                    if (item.Checked)
                    {

                        var associar = new AssociadoModelEmpresaModel()
                        {
                            EmpresaId = IdEmpresa,
                            AssociadoId = item.Id
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

        public List<CheckBoxViewModel> GetAssociadosEmpresa(int IdEmpresa)
        {
            var lstAssociados = new List<CheckBoxViewModel>();

            try
            {

                var AssociadosEmpresa = from c in _context.Associados
                                        select new CheckBoxViewModel
                                        {
                                            Id = c.Id,
                                            Nome = c.Nome,
                                            Checked = _context.AssociadosEmpresa
                                                        .Any(ce => ce.EmpresaId == IdEmpresa && ce.AssociadoId == c.Id)
                                        };

                lstAssociados = AssociadosEmpresa.ToList();

            }
            catch(Exception ex)
            {
                throw ex;
            }


            return lstAssociados;
        }

    }
}
