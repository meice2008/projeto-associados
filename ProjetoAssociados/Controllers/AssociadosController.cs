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
        public IActionResult Index()
        {
            IEnumerable<AssociadoModel> associados = _context.Associados;
            return View(associados);
        }
    }
}
