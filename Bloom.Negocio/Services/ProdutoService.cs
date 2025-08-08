using Bloom.Negocio.Interfaces;
using Bloom.Negocio.Models;
using Bloom.Negocio.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (_produtoRepository.Buscar(f => f.Id == produto.Id).Result.Any())
            {
                Notificar("Já existe um produto com este id informado.");
                return;
            }

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Atualizar(produto);
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
