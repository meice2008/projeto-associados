using Microsoft.EntityFrameworkCore;
using ProjetoAssociados.Data;
using ProjetoAssociados.Models;

namespace ProjetoAssociados.Services.AssociadoServices
{
    public class AssociadoServices : IAssociadoServices
    {
        readonly private ApplicationDbContext _context;
        public AssociadoServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void DeleteAssociado(int id)
        {
            AssociadoModel Associado = await GetAssociadoById(id);

            try
            {
                _context.Associados.Remove(Associado);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AssociadoModel> GetAssociadoById(int? id)
        {
            var associado = _context.Associados.FirstOrDefaultAsync(x => x.Id == id).Result;
            return associado;            
        }

        public async Task<IEnumerable<AssociadoModel>> GetAssociados()
        {
            var associados = _context.Associados;
            return associados;
        }



        
    }
}
