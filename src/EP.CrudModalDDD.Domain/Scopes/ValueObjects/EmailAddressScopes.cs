using DomainNotificationHelper.Validation;
using EP.CrudModalDDD.Domain.ValueObjects;
using SharedKernel.Domain.Model;

namespace EP.CrudModalDDD.Domain.Scopes.ValueObjects
{
    public static class EmailAddressScopes
    {
        const string Pattern = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        public static bool IsValid(this EmailAddress emailAddress)
        {
            if (emailAddress.Address == null)
                emailAddress = new EmailAddress("");

            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertMatches(Pattern, emailAddress.Address, "E-mail está inválido.")
            );

        }
    }
}
