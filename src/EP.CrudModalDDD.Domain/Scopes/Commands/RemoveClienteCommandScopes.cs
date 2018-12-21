using DomainNotificationHelper.Validation;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Interfaces.Repository;

namespace EP.CrudModalDDD.Domain.Scopes.Commands
{
    public static class RemoveClienteCommandScopes
    {
        public static bool PossuiEnderecoInformado(this RemoveClienteCommand command, IClienteRepository clienteRepository)
        {
            var cliente = clienteRepository.ObterPorId(command.ClienteId);

            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(cliente.Enderecos.Count == 0,
                    "Cliente possui endereços. Não será possível excluir.")
            );
        }

    }
}
