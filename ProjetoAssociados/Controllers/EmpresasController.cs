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
            var AssociadosEmpresa = _context.Associados;

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
                var empresa = new EmpresaModel();
                empresa.Nome = empresaViewModel.Nome;
                empresa.Cnpj = empresaViewModel.Cnpj;
                empresa.Associados = new List<AssociadoModel>();

                _context.Empresas.Add(empresa);
                _context.SaveChanges();


                foreach (var item in empresaViewModel.Associados)
                {
                    if (item.Checked)
                    {
                        _context.AssociadosEmpresa.AddRange(new AssociadoModelEmpresaModel()
                        {
                            EmpresaId = empresa.Id, //empresaViewModel.Id,
                            AssociadoId = item.Id
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

            if(Id == null || Id == 0)
            {
                return NotFound();
            }

            EmpresaModel empresaModel = _context.Empresas.FirstOrDefault(x => x.Id == Id);

            if(empresaModel == null)
            {
                return NotFound();
            }

            var AssociadosEmpresa = from c in _context.Associados
                                    select new
                                    {
                                        c.Id,
                                        c.Nome,
                                        Checked = ((from ce in _context.AssociadosEmpresa
                                                    where (ce.EmpresaId == Id) & (ce.AssociadoId == c.Id)
                                                    select ce).Count() > 0)
                                    };

            var empresaViewModel = new EmpresaViewModel();

            empresaViewModel.Id = Id.Value;
            empresaViewModel.Nome = empresaModel.Nome;
            empresaViewModel.Cnpj = empresaModel.Cnpj;

            var checkboxListAssociados = new List<CheckBoxViewModel>();

            foreach(var item in AssociadosEmpresa)
            {
                checkboxListAssociados.Add(new CheckBoxViewModel { Id = item.Id, Nome = item.Nome, Checked = item.Checked});
            }

            empresaViewModel.Associados = checkboxListAssociados;

            return View(empresaViewModel);
        }

        [HttpPost]
        public IActionResult Editar(EmpresaViewModel empresaViewModel)
        {
            if (ModelState.IsValid)
            {
                var empresaSelecionada = _context.Empresas.Find(empresaViewModel.Id);
                empresaSelecionada.Nome = empresaViewModel.Nome;
                empresaSelecionada.Cnpj = empresaViewModel.Cnpj;

                foreach (var item in _context.AssociadosEmpresa)
                {
                    if (item.EmpresaId == empresaViewModel.Id)
                    {
                        _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    }
                }

                foreach(var item in empresaViewModel.Associados)
                {
                    if (item.Checked)
                    {
                        _context.AssociadosEmpresa.Add(new AssociadoModelEmpresaModel()
                        {
                            EmpresaId = empresaViewModel.Id, 
                            AssociadoId = item.Id
                        });
                    }
                }


                //_context.Empresas.Update(empresaModel);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Editado com sucesso!!";

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Erro ao realizar edição!! Tente novamente";

            return View(empresaViewModel);
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

            TempData["MensagemSucesso"] = "Excluido com sucesso!!";

            return RedirectToAction("Index");

        }

    }
}
