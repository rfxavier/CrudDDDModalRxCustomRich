using System;
using System.Collections.Generic;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public class AdicionaNovoClienteCommand : ClienteCommand
    { 
        public AdicionaNovoClienteCommand(Guid clienteId, string nome, string emailAddress, string cpfNumero, DateTime? dataNascimento, bool ativo)
        {
            ClienteId = clienteId;
            Nome = nome;
            EmailAddress = emailAddress;
            CpfNumero = cpfNumero;
            DataNascimento = dataNascimento;
            Ativo = ativo;

            Enderecos = new List<ClienteEnderecoCommand>();
        }
    }
}
