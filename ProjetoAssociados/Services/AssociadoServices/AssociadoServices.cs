using Newtonsoft.Json;
using ProjetoAssociados.Models;
using System.Text;

namespace ProjetoAssociados.Services.AssociadoServices
{
    public class AssociadoServices : IAssociadoServices
    {
        //readonly private ApplicationDbContext _context;
        //private readonly IEmpresaServices _empresaInterface;
        private readonly IConfiguration _config;
        public AssociadoServices(IConfiguration config)
        {
            //_context = context;
            //_empresaInterface = empresaInterface;
            _config = config;
        }

        public async void DeleteAssociado(int id)
        {
            //const string apiUrl = "https://localhost:7063/api/Associado/";
            var urlApiAssociado = _config.GetSection("ApiSettings:AssociadoApiUrl");

            AssociadoModel associado = GetAssociadoById(id).Result.Dados;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.DeleteAsync(urlApiAssociado.Value+ "/" + associado.Id);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<List<AssociadoModel>>>(jsonResponse);

                    //return serviceResponse.Dados;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }        

        public async Task<ServiceResponse<AssociadoModel>> GetAssociadoById(int? id)
        {

            //const string apiUrl = "https://localhost:7063/api/Associado/";
            var urlApiAssociado = _config.GetSection("ApiSettings:AssociadoApiUrl");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(urlApiAssociado.Value + "/" + id);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<AssociadoModel>>(jsonResponse);

                    return serviceResponse;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }

        public async Task<ServiceResponse<List<AssociadoModel>>> GetAssociados()
        {

            //const string apiUrl = "https://localhost:7063/api/Associado";
            var urlApiAssociado = _config.GetSection("ApiSettings:AssociadoApiUrl");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(urlApiAssociado.Value);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<List<AssociadoModel>>>(jsonResponse);

                    return serviceResponse;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public async Task<AssociadoViewModel> Editar(AssociadoViewModel associadoViewModel)
        {
            //const string apiUrl = "https://localhost:7063/api/Associado/";
            var urlApiAssociado = _config.GetSection("ApiSettings:AssociadoApiUrl");

            using (var httpClient = new HttpClient())
            {
                try
                {

                    string associadoJson = JsonConvert.SerializeObject(associadoViewModel);

                    HttpContent content = new StringContent(associadoJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PutAsync(urlApiAssociado.Value + "/" + associadoViewModel.Id, content);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<AssociadoViewModel>>(jsonResponse);

                    return serviceResponse.Dados;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public async Task<AssociadoViewModel> GetEditar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                throw new NotImplementedException();
            }

            AssociadoModel associado = GetAssociadoById(Id).Result.Dados; 

            if (associado == null)
            {
                throw new NotImplementedException();
            }

            var EmpresasAssociado = GetEmpresasAssociado(associado.Id).Result.Dados;

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
            //const string apiUrl = "https://localhost:7063/api/Associado";
            var urlApiAssociado = _config.GetSection("ApiSettings:AssociadoApiUrl");

            try
            {

                using (var httpClient = new HttpClient())
                {
                    try
                    {

                        string associadoJson = JsonConvert.SerializeObject(associadoViewModel);

                        HttpContent content = new StringContent(associadoJson, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await httpClient.PostAsync(urlApiAssociado.Value, content);
                        response.EnsureSuccessStatusCode();

                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<List<AssociadoModel>>>(jsonResponse);

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


        public async Task<ServiceResponse<List<CheckBoxViewModel>>> GetEmpresasAssociado(int IdAssociado)
        {

            //const string apiUrl = "https://localhost:7063/api/Associado/GetEmpresasAssociado/";
            var urlApiAssociado = _config.GetSection("ApiSettings:AssociadoApiUrl");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(urlApiAssociado.Value + "/GetEmpresasAssociado/" + IdAssociado);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<List<CheckBoxViewModel>>>(jsonResponse);


                    return serviceResponse;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

    }
}
