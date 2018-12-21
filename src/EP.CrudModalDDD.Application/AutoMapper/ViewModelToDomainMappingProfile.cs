using AutoMapper;
using EP.CrudModalDDD.Application.ViewModels;
using EP.CrudModalDDD.Domain.Entities;

namespace EP.CrudModalDDD.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<ClienteEnderecoViewModel, Cliente>();
            CreateMap<EnderecoViewModel, Endereco>();
            CreateMap<ClienteEnderecoViewModel, Endereco>();
            CreateMap<CidadeViewModel, Cidade>();
        }
    }
}