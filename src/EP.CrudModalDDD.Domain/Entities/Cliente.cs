using EP.CrudModalDDD.Domain.Scopes.Entities;
using EP.CrudModalDDD.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedKernel.Domain.Model;

namespace EP.CrudModalDDD.Domain.Entities
{
    public class Cliente
    {
        private readonly IList<Endereco> _enderecos;
        // Empty constructor for EF
        protected Cliente()
        {
            _enderecos = new List<Endereco>();
        }

        public Cliente(Guid clienteId, string nome, EmailAddress email, CPF cpf, DateTime? dataNascimento, bool ativo)
        {
            ClienteId = clienteId;
            Nome = nome;
            Email = email;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Ativo = ativo;
            _enderecos = new List<Endereco>();
        }

        public Guid ClienteId { get; private set; }
        public string Nome { get; private set; }
        public EmailAddress Email { get; private set; }
        public CPF CPF { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }
        public ICollection<Endereco> Enderecos { get { return _enderecos.ToArray(); } }

        public bool RegistrarNovo()
        {
            return this.RegistrarNovoScopeEstaValido();
            //Ativo = false;
        }

        public void Atualizar()
        {
        }

        public void Remover()
        {
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            _enderecos.Add(endereco);
        }
    }
}