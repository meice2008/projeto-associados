using Newtonsoft.Json;
using ProjetoAssociados.Models;
using System.Text;

namespace ProjetoAssociados.Services.EmpresaServices
{
    public class EmpresaServices : IEmpresaServices
    {
        //readonly private ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public EmpresaServices(IConfiguration config)
        {
            //_context = context;
            _config = config;
        }

        public async void DeleteEmpresa(int id)
        {

            //const string apiUrl = "https://localhost:7063/api/Empresa/";
            var urlApiEmpresa = _config.GetSection("ApiSettings:EmpresaApiUrl");

            EmpresaModel empresa = await GetEmpresaById(id);

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.DeleteAsync(urlApiEmpresa.Value +"/"+ empresa.Id);
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


        }

        public async Task<EmpresaModel> GetEmpresaById(int? id)
        {

            //const string apiUrl = "https://localhost:7063/api/Empresa/";
            var urlApiEmpresa = _config.GetSection("ApiSettings:EmpresaApiUrl");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(urlApiEmpresa.Value + "/" + id);
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

        }

        public async Task<IEnumerable<EmpresaModel>> GetEmpresas()
        {

            //const string apiUrl = "https://localhost:7063/api/Empresa";
            var urlApiEmpresa = _config.GetSection("ApiSettings:EmpresaApiUrl");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(urlApiEmpresa.Value);
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

        }

        public async Task<IEnumerable<AssociadoModelEmpresaModel>> GetEmpresasAssociado()
        {

            //const string apiUrl = "https://localhost:7063/api/Empresa/GetEmpresasAssociado";
            var urlApiEmpresa = _config.GetSection("ApiSettings:EmpresaApiUrl");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(urlApiEmpresa.Value + "/GetEmpresasAssociado");
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<List<AssociadoModelEmpresaModel>>>(jsonResponse);


                    return serviceResponse.Dados;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

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

            var AssociadosEmpresa = GetAssociadosEmpresa(empresaModel.Id).Result;

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

            //const string apiUrl = "https://localhost:7063/api/Empresa/";
            var urlApiEmpresa = _config.GetSection("ApiSettings:EmpresaApiUrl");

            using (var httpClient = new HttpClient())
            {
                try
                {

                    string empresaJson = JsonConvert.SerializeObject(empresaViewModel);

                    HttpContent content = new StringContent(empresaJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PutAsync(urlApiEmpresa.Value + "/" + empresaViewModel.Id, content);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<EmpresaViewModel>>(jsonResponse);

                    return serviceResponse.Dados;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }

        public async void Cadastrar(EmpresaViewModel empresaViewModel)
        {
            //const string apiUrl = "https://localhost:7063/api/Empresa";
            var urlApiEmpresa = _config.GetSection("ApiSettings:EmpresaApiUrl");

            try
            {                                

                using (var httpClient = new HttpClient())
                {
                    try
                    {

                        string empresaJson = JsonConvert.SerializeObject(empresaViewModel);

                        HttpContent content = new StringContent(empresaJson, Encoding.UTF8, "application/json");                        

                        HttpResponseMessage response = await httpClient.PostAsync(urlApiEmpresa.Value, content);
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<CheckBoxViewModel>> GetAssociadosEmpresa(int IdEmpresa)
        {

            //const string apiUrl = "https://localhost:7063/api/Empresa/GetAssociadosEmpresa/";
            var urlApiEmpresa = _config.GetSection("ApiSettings:EmpresaApiUrl");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(urlApiEmpresa.Value + "/GetAssociadosEmpresa/" + IdEmpresa);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<List<CheckBoxViewModel>>>(jsonResponse);


                    return serviceResponse.Dados;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }

    }
}
