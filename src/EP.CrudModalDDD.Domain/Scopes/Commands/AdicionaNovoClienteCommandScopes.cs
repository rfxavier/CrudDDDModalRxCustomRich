using DomainNotificationHelper.Validation;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Interfaces.Repository;

namespace EP.CrudModalDDD.Domain.Scopes.Commands
{
    public static class AdicionaNovoClienteCommandScopes
    {
        public static bool PossuiCpfUnico(this AdicionaNovoClienteCommand command, IClienteRepository clienteRepository)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(clienteRepository.ObterPorCpf(command.CpfNumero) == null,
                    "CPF já cadastrado! Esqueceu sua senha?")
            );
        }

        public static bool PossuiEmailUnico(this AdicionaNovoClienteCommand command, IClienteRepository clienteRepository)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(clienteRepository.ObterPorEmail(command.EmailAddress) == null,
                    "E-mail já cadastrado! Esqueceu sua senha?")
            );
        }
    }
}
