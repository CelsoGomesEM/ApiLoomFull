using Bloom.Negocio.Interfaces;
using Bloom.Negocio.Models;
using Bloom.Negocio.Notificacoes;
using Bloom.Negocio.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloom.Testes
{

    class NotificadorFake : INotificador
    {
        public void Handle(Notificacao notificacao) { }
        public bool TemNotificacao() => true;
        public List<Notificacao> ObterNotificacoes() => new List<Notificacao>();
    }

    public class TestesProdutoComMoq
    {
        //Forçando Moq para retornar que o produto ja foi cadastrado com mesmo nome na mesma categoria
        [Fact]
        public async Task AdicionarDeveFalharQuandoNomeDuplicadoNaMesmaCategoria()
        {
            var repo = new Mock<IProdutoRepository>(MockBehavior.Strict);
            var notificador = new Mock<INotificador>();

            var categoriaId = Guid.NewGuid();
            var produto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = "Camiseta Londres",
                PrecoUnitario = 59.90m,
                CategoriaId = categoriaId
            };

            repo.Setup(r => r.ExisteEsteNomeProdutoEmCategoria(categoriaId, "Camiseta"))
                .ReturnsAsync(true);

            repo.Setup(r => r.Adicionar(It.IsAny<Produto>())).Throws(new Exception("Não deveria chamar Adicionar"));

            var service = new ProdutoService(notificador.Object, repo.Object);

            await service.Adicionar(produto);

            repo.Verify(r => r.ExisteEsteNomeProdutoEmCategoria(categoriaId, "Camiseta"), Times.Once);
            repo.Verify(r => r.Adicionar(It.IsAny<Produto>()), Times.Never);

            notificador.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.AtLeastOnce);
        }
    }
}
