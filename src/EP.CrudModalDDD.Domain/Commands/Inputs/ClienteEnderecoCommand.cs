using EP.CrudModalDDD.Domain.Interfaces;
using System;

namespace EP.CrudModalDDD.Domain.Commands.Inputs
{
    public class ClienteEnderecoCommand : ICommand
    {
        public Guid EnderecoId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Guid ClienteId { get; set; }

    }
}