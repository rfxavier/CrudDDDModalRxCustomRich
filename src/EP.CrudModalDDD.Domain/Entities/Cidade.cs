using System;
using SharedKernel.Domain.Model;

namespace EP.CrudModalDDD.Domain.Entities
{
    public class Cidade : Entity
    {
        public Cidade(Guid id, string nome, string uf, string codigoIbge)
        {
            Id = id;
            Nome = nome;
            UF = uf;
            CodigoIbge = codigoIbge;
        }

        protected Cidade()
        {
            
        }

        public Guid Id { get; set; }
        public string Nome { get; private set; }
        public string UF { get; private set; }
        public string CodigoIbge { get; private set; }
    }
}