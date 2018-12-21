using SharedKernel.Domain.Model;

namespace EP.CrudModalDDD.Domain.ValueObjects
{
    public class CPF: ValueObject<CPF>
    {
        public string Numero { get; private set; }

        protected CPF() { }

        public CPF(string numero)
        {
            this.Numero = numero;
        }
    }
}
