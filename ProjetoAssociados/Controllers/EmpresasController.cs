using Microsoft.AspNetCore.Mvc;
using ProjetoAssociados.Data;
using ProjetoAssociados.Models;

namespace ProjetoAssociados.Controllers
{
    public class EmpresasController : Controller
    {
        readonly private ApplicationDbContext _context;

        public EmpresasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<EmpresaModel> empresas = _context.Empresas;
            return View(empresas);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(EmpresaModel empresaModel)
        {
            if(ModelState.IsValid)
            {
                _context.Empresas.Add(empresaModel);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? Id)
        {

            if(Id == null || Id == 0)
            {
                return NotFound();
            }

            EmpresaModel empresaModel = _context.Empresas.FirstOrDefault(x => x.Id == Id);

            if(empresaModel == null)
            {
                return NotFound();
            }

            return View(empresaModel);
        }

        [HttpPost]
        public IActionResult Editar(EmpresaModel empresaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Empresas.Update(empresaModel);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(empresaModel);
        }

        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if(id == null || id ==0)
            {
                return NotFound();
            }

            EmpresaModel empresaModel = _context.Empresas.FirstOrDefault(x=>x.Id == id);

            if(empresaModel == null)
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

            _context.Empresas.Remove(empresaModel);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}
