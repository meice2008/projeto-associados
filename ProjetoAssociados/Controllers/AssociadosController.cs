using Microsoft.AspNetCore.Mvc;
using ProjetoAssociados.Data;
using ProjetoAssociados.Models;
using ProjetoAssociados.Services.AssociadoServices;
using ProjetoAssociados.Services.EmpresaServices;

namespace ProjetoAssociados.Controllers
{
    public class AssociadosController : Controller
    {
        //readonly private ApplicationDbContext _context;
        private readonly IEmpresaServices _empresaInterface;
        private readonly IAssociadoServices _associadoInterface;

        public AssociadosController(IEmpresaServices empresaInterface, IAssociadoServices associadoInterface)
        {
            //_context = context;
            _empresaInterface = empresaInterface;
            _associadoInterface = associadoInterface;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<AssociadoModel> associados = _associadoInterface.GetAssociados().Result; 
            return View(associados);
        }
        

        [HttpGet]
        public IActionResult Cadastrar()
        {

            var AssociadosEmpresa = _empresaInterface.GetEmpresas().Result; 

            var empresaViewModel = new AssociadoViewModel();
            var checkboxListAssociados = new List<CheckBoxViewModel>();

            foreach (var item in AssociadosEmpresa)
            {
                checkboxListAssociados.Add(new CheckBoxViewModel { Id = item.Id, Nome = item.Nome, Checked = false });
            }

            empresaViewModel.Empresas = checkboxListAssociados;

            return View(empresaViewModel);
        }

        [HttpPost]
        public IActionResult Cadastrar(AssociadoViewModel associadoViewModel)
        {
            if (ModelState.IsValid)
            {
                _associadoInterface.Cadastrar(associadoViewModel);

                TempData["MensagemSucesso"] = "Cadastrado com sucesso!!";

                return RedirectToAction("Index");
            }

            return View();

        }


        [HttpGet]
        public IActionResult Editar(int? Id)
        {

            var associadoViewModel = _associadoInterface.GetEditar(Id).Result;
            return View(associadoViewModel);

        }

        [HttpPost]
        public IActionResult Editar(AssociadoViewModel associadoViewModel)
        {
            AssociadoViewModel associadoEditada = new AssociadoViewModel();

            if (ModelState.IsValid)
            {
                associadoEditada = _associadoInterface.Editar(associadoViewModel).Result;

                TempData["MensagemSucesso"] = "Editado com sucesso!!";

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Erro ao realizar edição!! Tente novamente";

            return View(associadoEditada);
        }

        [HttpGet]
        public IActionResult Excluir(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            AssociadoModel associado = _associadoInterface.GetAssociadoById(Id).Result; 

            if (associado == null)
            {
                return NotFound();
            }

            return View(associado);
        }

        [HttpPost]
        public IActionResult Excluir(AssociadoModel associadoModel)
        {
            if (associadoModel == null)
            {
                return NotFound();
            }

            _associadoInterface.DeleteAssociado(associadoModel.Id);            


            return RedirectToAction("Index");
        }


    }
}