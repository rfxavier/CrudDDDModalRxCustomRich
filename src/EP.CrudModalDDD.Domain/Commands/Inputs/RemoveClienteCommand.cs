using System;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public class RemoveClienteCommand : ClienteCommand
    {
        public RemoveClienteCommand(Guid clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
