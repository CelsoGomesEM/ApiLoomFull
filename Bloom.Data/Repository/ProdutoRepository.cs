using Bloom.Data.DbContext;
using Bloom.Negocio.Exceptions;
using Bloom.Negocio.Interfaces;
using Bloom.Negocio.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloom.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApiDBContext db) : base(db){}
        public async Task<bool> ExisteEsteNomeProdutoEmCategoria(Guid categoriaId, string nome)
        {
            var nomeNorm = nome.Trim().ToUpper();
            return await DbSet.AsNoTracking()
                .AnyAsync(p => p.CategoriaId == categoriaId &&
                               p.Nome.ToUpper() == nomeNorm);
        }

        public override async Task Adicionar(Produto entity)
        {
            if (await ExisteEsteNomeProdutoEmCategoria(entity.CategoriaId, entity.Nome))
                throw new NomeDuplicadoException();

            try
            {
                await base.Adicionar(entity); 
            }
            catch (DbUpdateException ex) when (IsUniqueConstraint(ex, "UX_Produto_Categoria_Nome"))
            {
                throw new NomeDuplicadoException();
            }
        }

        public override async Task Atualizar(Produto entity)
        {
            if (await ExisteEsteNomeProdutoEmCategoria(entity.CategoriaId, entity.Nome))
                throw new NomeDuplicadoException();

            try
            {
                await base.Atualizar(entity);
            }
            catch (DbUpdateException ex) when (IsUniqueConstraint(ex, "UX_Produto_Categoria_Nome"))
            {
                throw new NomeDuplicadoException();
            }
        }

        private static bool IsUniqueConstraint(DbUpdateException ex, string indexName)
        {
            if (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
                return ex.InnerException.Message?.Contains(indexName, StringComparison.OrdinalIgnoreCase) == true;
            return false;
        }
    }
}
