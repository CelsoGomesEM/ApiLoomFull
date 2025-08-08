using AutoMapper;
using Bloom.Api.ViewModel;
using Bloom.Data.Repository;
using Bloom.Negocio.Interfaces;
using Bloom.Negocio.Models;
using Bloom.Negocio.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloom.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        public ProdutosController(INotificador notificador, IProdutoRepository produtoRepository, IProdutoService produtoService, IMapper mapper) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var produtos = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
            return produtos;
        }

        [HttpGet("paginado")]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> ObterTodos(Guid? categoriaId, int page, int size)
        {
            if (page <= 0 || size <= 0)
                return BadRequest("Parâmetros page e size devem ser maiores que zero.");

            var query = _produtoRepository.Query();

            if (categoriaId.HasValue)
                query = query.Where(p => p.CategoriaId == categoriaId.Value);

            var produtos = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            var produtosVm = _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);

            return Ok(produtosVm);
        }


        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var categoria = _mapper.Map<Produto>(produtoViewModel);

            await _produtoService.Adicionar(categoria);

            return CustomResponse(produtoViewModel);
        }
    }
}
