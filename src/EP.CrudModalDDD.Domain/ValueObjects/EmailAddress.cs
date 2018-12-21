using SharedKernel.Domain.Model;

namespace EP.CrudModalDDD.Domain.ValueObjects
{
    public class EmailAddress : ValueObject<EmailAddress>
    {
        const string PATTERN = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        public string Address { get; private set; }

        protected EmailAddress() { }

        public EmailAddress(string address)
        {
            this.Address = address;
        }
    }
}
