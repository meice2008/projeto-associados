using Microsoft.AspNetCore.Mvc;
using ProjetoAssociados.Models;
using ProjetoAssociados.Services.AssociadoServices;
using ProjetoAssociados.Services.EmpresaServices;

namespace ProjetoAssociados.Controllers
{
    public class EmpresasController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IEmpresaServices _empresaInterface;
        private readonly IAssociadoServices _associadoInterface;

        public EmpresasController(IEmpresaServices empresaInterface, IAssociadoServices associadoInterface)
        {
            //_context = context;
            _empresaInterface = empresaInterface;
            _associadoInterface = associadoInterface;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var empresas = _empresaInterface.GetEmpresas().Result;

            return View(empresas);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var AssociadosEmpresa = _associadoInterface.GetAssociados().Result;

            var empresaViewModel = new EmpresaViewModel();
            var checkboxListAssociados = new List<CheckBoxViewModel>();

            foreach (var item in AssociadosEmpresa)
            {
                checkboxListAssociados.Add(new CheckBoxViewModel { Id = item.Id, Nome = item.Nome, Checked = false });
            }

            empresaViewModel.Associados = checkboxListAssociados;

            return View(empresaViewModel);
        }

        [HttpPost]
        public IActionResult Cadastrar(EmpresaViewModel empresaViewModel)
        {
            if (ModelState.IsValid)
            {

                _empresaInterface.Cadastrar(empresaViewModel);  

                TempData["MensagemSucesso"] = "Cadastrado com sucesso!!";

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? Id)
        {
            var empresaViewModel = _empresaInterface.GetEditar(Id).Result;

            return View(empresaViewModel);
        }

        [HttpPost]
        public IActionResult Editar(EmpresaViewModel empresaViewModel)
        {
            EmpresaViewModel empresaEditada = new EmpresaViewModel();

            if (ModelState.IsValid)
            {            
                empresaEditada = _empresaInterface.Editar(empresaViewModel).Result;

                TempData["MensagemSucesso"] = "Editado com sucesso!!";

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Erro ao realizar edição!! Tente novamente";

            return View(empresaEditada);
        }

        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if(id == null || id ==0)
            {
                return NotFound();
            }

            EmpresaModel empresaModel = _empresaInterface.GetEmpresaById(id).Result;  //_context.Empresas.FirstOrDefault(x=>x.Id == id);

            if (empresaModel == null)
            {
                return NotFound(empresaModel);
            }

            return View(empresaModel);
        }

        [HttpPost]
        public IActionResult Excluir (EmpresaModel empresaModel)
        {
            if (empresaModel == null)
            {
                return NotFound();
            }

            _empresaInterface.DeleteEmpresa(empresaModel.Id);

            TempData["MensagemSucesso"] = "Excluido com sucesso!!";

            return RedirectToAction("Index");

        }

    }
}
