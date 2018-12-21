using EP.CrudModalDDD.Domain.Interfaces;
using System;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public abstract class CidadeCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public string CodigoIbge { get; set; }

    }
}