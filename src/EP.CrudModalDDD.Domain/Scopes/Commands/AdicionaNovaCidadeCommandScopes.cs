using DomainNotificationHelper.Validation;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Interfaces.Repository;

namespace EP.CrudModalDDD.Domain.Scopes.Commands
{
    public static class AdicionaNovaCidadeCommandScopes
    {
        public static bool PossuiNomeCidadeInformado(this AdicionaNovaCidadeCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertTrue(command.Nome != string.Empty, "Nome da Cidade não informado") );
        }

        public static bool PossuiNomeCidadeUnico(this AdicionaNovaCidadeCommand command, ICidadeRepository cidadeRepository)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertTrue(cidadeRepository.ObterPorNome(command.Nome) == null, "Nome da Cidade já existe"));
        }

    }
}