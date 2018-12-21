using System;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public class AdicionaNovoEnderecoCommand : EnderecoCommand
    { 
        public AdicionaNovoEnderecoCommand(Guid clienteId, Guid enderecoId, string logradouro, string numero, string complemento,
            string bairro, string cep, string cidade, string estado)
        {
            ClienteId = clienteId;
            EnderecoId = enderecoId;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
        }
    }
}