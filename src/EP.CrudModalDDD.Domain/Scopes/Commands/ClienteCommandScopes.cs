using DomainNotificationHelper.Validation;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using System;
using System.Linq;

namespace EP.CrudModalDDD.Domain.Scopes.Commands
{
    public static class ClienteCommandScopes
    {
        #region Specifications Granulares

        public static bool EhMaiorDeIdade(this ClienteCommand command)
        {
            return command.DataNascimento == null || AssertionConcern.IsSatisfiedBy(
                       AssertionConcern.AssertTrue(DateTime.Now.Year - command.DataNascimento.Value.Year >= 18,
                           "Cliente não tem maioridade para cadastro."));
        }

        public static bool PossuiEnderecoInformado(this ClienteCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(command.Enderecos != null && command.Enderecos.Any(),
                    "Cliente não informou endereço")
            );
        }

        #endregion Specifications Granulares

        //#region Specifications Compostas

        //public static bool EstaAptoParaCadastro(this ClienteCommand command, IClienteRepository clienteRepository)
        //{
        //    return command.PossuiCpfUnico(clienteRepository) &
        //           command.PossuiEmailUnico(clienteRepository) &
        //           command.PossuiEnderecoInformado();
        //}
        //#endregion
    }
}
