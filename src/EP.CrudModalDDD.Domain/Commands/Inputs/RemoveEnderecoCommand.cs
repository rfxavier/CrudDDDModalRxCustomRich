using EP.CrudModalDDD.Domain.Interfaces;
using System;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public class RemoveEnderecoCommand : ICommand
    {
        public RemoveEnderecoCommand(Guid enderecoId)
        {
            EnderecoId = enderecoId;
        }

        public Guid EnderecoId { get; set; }
    }
}