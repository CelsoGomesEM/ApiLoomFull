using Bloom.Negocio.Models;
using Bloom.Negocio.Models.Validations;
using FluentValidation.TestHelper;

namespace Bloom.Testes
{
    public class TestesProduto
    {
        [Fact]
        public void PrecoUnitarioDeveSerInvalidoQuandoNegativo()
        {
            var validator = new ProdutoValidation();

            var produto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = "Camisa Real Madrid",
                PrecoUnitario = -1m,
                CategoriaId = Guid.NewGuid()
            };

            var result = validator.TestValidate(produto);

            result.ShouldHaveValidationErrorFor(p => p.PrecoUnitario);
        }

        [Fact]
        public void PrecoUnitarioDeveSerValidoQuandoZeroOuMaior()
        {
            var validator = new ProdutoValidation();

            var p0 = new Produto { Id = Guid.NewGuid(), Nome = "Camisa Real Madrid", PrecoUnitario = 0m, CategoriaId = Guid.NewGuid() };
            var p1 = new Produto { Id = Guid.NewGuid(), Nome = "Camisa Barcelona", PrecoUnitario = 10.5m, CategoriaId = Guid.NewGuid() };

            validator.TestValidate(p0).ShouldNotHaveValidationErrorFor(p => p.PrecoUnitario);
            validator.TestValidate(p1).ShouldNotHaveValidationErrorFor(p => p.PrecoUnitario);
        }
    }
}