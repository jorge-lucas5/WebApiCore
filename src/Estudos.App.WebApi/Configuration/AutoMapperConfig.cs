using AutoMapper;
using Estudos.App.Business.Models;
using Estudos.App.WebApi.ViewModels;

namespace Estudos.App.WebApi.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<ProdutoViewModel, Produto>();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor,
                    opt=> opt.MapFrom(src=> src.Fornecedor.Nome));
        }
    }
}