using Bloom.Negocio.Exceptions;
using Bloom.Negocio.Interfaces;
using Bloom.Negocio.Models;
using Bloom.Negocio.Models.Validations;

namespace Bloom.Negocio.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(INotificador notificador, 
            IProdutoRepository produtoRepository) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            try
            {
                await _produtoRepository.Adicionar(produto);
            }
            catch (NomeDuplicadoException)
            {
                Notificar("Já existe um produto com esse nome nessa categoria.");
            }
        }

        public async Task Atualizar(Produto produto)
        {
            try
            {
                if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

                await _produtoRepository.Atualizar(produto);
            }
            catch(NomeDuplicadoException)
            {
                Notificar("Já existe um produto com esse nome nessa categoria.");
            }
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
