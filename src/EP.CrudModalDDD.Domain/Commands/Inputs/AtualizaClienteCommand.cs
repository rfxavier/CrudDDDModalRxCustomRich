using System;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public class AtualizaClienteCommand : ClienteCommand
    { 
        public AtualizaClienteCommand(Guid clienteId, string nome, string emailAddress, string cpfNumero, DateTime? dataNascimento, bool ativo)
        {
            ClienteId = clienteId;
            Nome = nome;
            EmailAddress = emailAddress;
            CpfNumero = cpfNumero;
            DataNascimento = dataNascimento;
            Ativo = ativo;
        }

    }
}
