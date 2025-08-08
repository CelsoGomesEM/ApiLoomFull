using Bloom.Data.DbContext;
using Bloom.Negocio.Interfaces;
using Bloom.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloom.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApiDBContext db) : base(db) { }
    }
}
