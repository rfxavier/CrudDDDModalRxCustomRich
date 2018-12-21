using System;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public class AdicionaNovaCidadeCommand : CidadeCommand
    { 
        public AdicionaNovaCidadeCommand(Guid id, string nome, string uF, string codigoIbge)
        {
            Id = id;
            Nome = nome;
            UF = uF;
            CodigoIbge = codigoIbge;
        }

    }
}