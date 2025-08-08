using Bloom.Negocio.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloom.Data.DbContext
{
    public class ApiDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Esta linha faz com que o EF Core encontre todas as classes de mapping (como CategoriaMapping)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDBContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
