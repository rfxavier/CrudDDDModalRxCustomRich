using System;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public class RemoveCidadeCommand : CidadeCommand
    {
        public RemoveCidadeCommand(Guid id)
        {
            Id = id;
        }
    }
}
