using DomainNotificationHelper.Validation;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Interfaces.Repository;
using System;

namespace EP.CrudModalDDD.Domain.Scopes.Commands
{
    public static class AtualizaClienteCommandScopes
    {
        public static bool PossuiCpfUnico(this AtualizaClienteCommand command, IClienteRepository clienteRepository)
        {
            var clienteId = Guid.Empty;

            var cliente = clienteRepository.ObterPorCpf(command.CpfNumero);

            if (cliente != null)
            {
                clienteId = cliente.ClienteId;
            }

            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(cliente == null || clienteId == command.ClienteId,
                    "CPF já cadastrado! Esqueceu sua senha?")
            );
        }

        public static bool PossuiEmailUnico(this AtualizaClienteCommand command, IClienteRepository clienteRepository)
        {
            var clienteId = Guid.Empty;

            var cliente = clienteRepository.ObterPorEmail(command.EmailAddress);

            if (cliente != null)
            {
                clienteId = cliente.ClienteId;
            }

            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(cliente == null || clienteId == command.ClienteId,
                    "E-mail já cadastrado! Esqueceu sua senha?")
            );
        }
    }
}
