namespace Bloom.Api.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProdutoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0, double.MaxValue, ErrorMessage = "O campo {0} precisa ser maior ou igual a {1}")]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CategoriaId { get; set; }
    }

}
