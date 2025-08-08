using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloom.Negocio.Exceptions
{
    public class NomeDuplicadoException : Exception
    {
        public NomeDuplicadoException()
            : base("Já existe um produto com esse nome nessa categoria.") { }

        public NomeDuplicadoException(string message)
            : base(message) { }

        public NomeDuplicadoException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
