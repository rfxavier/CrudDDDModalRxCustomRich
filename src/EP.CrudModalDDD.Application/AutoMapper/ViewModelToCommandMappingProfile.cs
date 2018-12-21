using AutoMapper;
using EP.CrudModalDDD.Application.ViewModels;
using EP.CrudModalDDD.Domain.Commands.Inputs;

namespace EP.CrudModalDDD.Application.AutoMapper
{
    public class ViewModelToCommandMappingProfile : Profile
    { 
        public ViewModelToCommandMappingProfile()
        {
            CreateMap<ClienteViewModel, AdicionaNovoClienteCommand>()
                .ForMember(c => c.EmailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(c => c.CpfNumero, opt => opt.MapFrom(src => src.CPF));

            CreateMap<ClienteEnderecoViewModel, AdicionaNovoClienteCommand>()
                .ForMember(c => c.EmailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(c => c.CpfNumero, opt => opt.MapFrom(src => src.CPF));


            CreateMap<ClienteViewModel, AtualizaClienteCommand>()
                .ForMember(c => c.EmailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(c => c.CpfNumero, opt => opt.MapFrom(src => src.CPF));

            CreateMap<ClienteEnderecoViewModel, AtualizaClienteCommand>()
                .ForMember(c => c.EmailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(c => c.CpfNumero, opt => opt.MapFrom(src => src.CPF));

            CreateMap<EnderecoViewModel, ClienteEnderecoCommand>();
            CreateMap<ClienteEnderecoViewModel, ClienteEnderecoCommand>();

            CreateMap<EnderecoViewModel, AdicionaNovoEnderecoCommand>();

            CreateMap<EnderecoViewModel, AtualizaEnderecoCommand>();

            CreateMap<CidadeViewModel, AdicionaNovaCidadeCommand>();
        }
    }
}