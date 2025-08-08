using AutoMapper;
using Bloom.Api.ViewModel;
using Bloom.Negocio.Models;

namespace Bloom.Api.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
