using AutoMapper;
using EP.CrudModalDDD.Application.ViewModels;
using EP.CrudModalDDD.Domain.DTO;
using EP.CrudModalDDD.Domain.Entities;

namespace EP.CrudModalDDD.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cliente, ClienteViewModel>()
                .ForMember(c => c.Email, opt => opt.MapFrom(src => src.Email.Address))
                .ForMember(c => c.CPF, opt => opt.MapFrom(src => src.CPF.Numero));
            CreateMap<Cliente, ClienteEnderecoViewModel>()
                .ForMember(c => c.Email, opt => opt.MapFrom(src => src.Email.Address))
                .ForMember(c => c.CPF, opt => opt.MapFrom(src => src.CPF.Numero));
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<Endereco, ClienteEnderecoViewModel>();
            CreateMap<Paged<Cliente>, PagedViewModel<ClienteViewModel>>();
            CreateMap<Paged<Cidade>, PagedViewModel<CidadeViewModel>>();
        }
    }
}