using EP.CrudModalDDD.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public abstract class ClienteCommand : ICommand
    {
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string EmailAddress { get; set; }
        public string CpfNumero { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public IList<ClienteEnderecoCommand> Enderecos { get; set; }
    }
}
