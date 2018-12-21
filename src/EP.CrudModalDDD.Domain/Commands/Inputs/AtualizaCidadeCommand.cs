using System;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public class AtualizaCidadeCommand : CidadeCommand
    {

      
        public AtualizaCidadeCommand(Guid id, string nome, string codigoIbge)
        {
            Id = id;
            Nome = nome;
            CodigoIbge = codigoIbge;
        }

    }
}
