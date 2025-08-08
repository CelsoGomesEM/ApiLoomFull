using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloom.Negocio.Models
{
    public class Produto : Entity
    {
        public string Nome { get; set; }
        public decimal PrecoUnitario { get; set; }
        public Categoria Categoria { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
