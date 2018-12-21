using System;

namespace EP.CrudModalDDD.Domain.Entities
{
    public class Endereco
    {
        // Empty constructor for EF
        protected Endereco()
        {
        }

        public Endereco(string logradouro, string numero, string complemento, string bairro, string cep, string cidade,
            string estado)
        {
            EnderecoId = Guid.NewGuid();
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
        }

        public Endereco(Guid clienteId, Guid enderecoId, string logradouro, string numero, string complemento, string bairro, string cep, string cidade,
            string estado)
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

        public Guid EnderecoId { get; protected set; }
        public string Logradouro { get; protected set; }
        public string Numero { get; protected set; }
        public string Complemento { get; protected set; }
        public string Bairro { get; protected set; }
        public string CEP { get; protected set; }
        public string Cidade { get; protected set; }
        public string Estado { get; protected set; }
        public Guid ClienteId { get; protected set; }
        public virtual Cliente Cliente { get; protected set; }
    }
}