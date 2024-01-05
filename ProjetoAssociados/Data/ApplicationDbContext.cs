using Microsoft.EntityFrameworkCore;
using ProjetoAssociados.Models;

namespace ProjetoAssociados.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<AssociadoModel> Associados { get; set; }
    }
}
