using Microsoft.AspNetCore.Mvc;
using ProjetoAssociados.Data;
using ProjetoAssociados.Models;

namespace ProjetoAssociados.Controllers
{
    public class AssociadosController : Controller
    {
        readonly private ApplicationDbContext _context;
        public AssociadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<AssociadoModel> associados = _context.Associados;
            return View(associados);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            var AssociadosEmpresa = _context.Empresas;

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
                            AssociadoId= associado.Id, //empresaViewModel.Id,
                            EmpresaId = item.Id
                        });

                    }
                }


                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastrado com sucesso!!";

                return RedirectToAction("Index");
            }

            return View();

        }


        [HttpGet]
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            AssociadoModel associado = _context.Associados.FirstOrDefault(x => x.Id == Id);

            if (associado == null)
            {
                return NotFound();
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

            return View(associadoViewModel);

        }

        [HttpPost]
        public IActionResult Editar(AssociadoViewModel associadoViewModel)
        {
            if (ModelState.IsValid)
            {
                var associadoSelecionado = _context.Associados.Find(associadoViewModel.Id);
                associadoSelecionado.Nome = associadoViewModel.Nome;
                associadoSelecionado.Cpf = associadoViewModel.Cpf;
                //associadoSelecionado.DtNascimento = associadoViewModel.DtNascimento

                foreach (var item in _context.AssociadosEmpresa)
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


                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Editado com sucesso!!";

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Erro ao realizar edição!! Tente novamente";

            return View(associadoViewModel);
        }

        [HttpGet]
        public IActionResult Excluir(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            AssociadoModel associado = _context.Associados.FirstOrDefault(x => x.Id == Id);

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

            _context.Associados.Remove(associadoModel);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
