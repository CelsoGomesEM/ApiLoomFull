using Bloom.Negocio.Interfaces;
using Bloom.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloom.Data.DbContext;

namespace Bloom.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApiDBContext db) : base(db){}
    }
}
