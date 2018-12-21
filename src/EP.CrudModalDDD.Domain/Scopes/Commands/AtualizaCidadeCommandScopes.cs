using DomainNotificationHelper.Validation;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Interfaces.Repository;
using System;

namespace EP.CrudModalDDD.Domain.Scopes.Commands
{
    public static class AtualizaCidadeCommandScopes
    {
        public static bool PossuiNomeCidadeInformado(this AtualizaCidadeCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertTrue(command.Nome != string.Empty, "Nome da Cidade não informado"));
        }

    
    }
}
