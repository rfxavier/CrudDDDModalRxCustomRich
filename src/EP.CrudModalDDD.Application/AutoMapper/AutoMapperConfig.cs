using AutoMapper;
using EP.CrudModalDDD.Domain.ValueObjects;

namespace EP.CrudModalDDD.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<string, EmailAddress>().ConvertUsing(s => new EmailAddress(s));
                x.CreateMap<string, CPF>().ConvertUsing(s => new CPF(s));
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
                x.AddProfile<ViewModelToCommandMappingProfile>();
            });
        }
    }
}