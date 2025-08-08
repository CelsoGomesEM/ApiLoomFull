using AutoMapper;
using Bloom.Api.ViewModel;
using Bloom.Data.Repository;
using Bloom.Negocio.Interfaces;
using Bloom.Negocio.Models;
using Bloom.Negocio.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloom.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriasController : MainController
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriasController(INotificador notificador, ICategoriaService categoriaService, ICategoriaRepository categoriaRepository, IMapper mapper) : base(notificador)
        {
            _categoriaService = categoriaService;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriaViewModel>> ObterTodos()
        {
            var categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
            return categorias;
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaViewModel>> Adicionar(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);

            await _categoriaService.Adicionar(categoria);

            return CustomResponse(categoriaViewModel);
        }
    }
}
