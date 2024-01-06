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
        public DbSet<EmpresaModel> Empresas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmpresaModel>(entity =>
            {
                entity.HasIndex(e => e.Cnpj).IsUnique();
            });

            modelBuilder.Entity<AssociadoModel>(entity =>
            {
                entity.HasIndex(e => e.Cpf).IsUnique();
            });
        }

    }
}
