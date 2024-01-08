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

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(AssociadoModel associadoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Associados.Add(associadoModel);
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

            return View(associado);
        }

        [HttpPost]
        public IActionResult Editar(AssociadoModel associadoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Associados.Update(associadoModel);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(associadoModel);
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
