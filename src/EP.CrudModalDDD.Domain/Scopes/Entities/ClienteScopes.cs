using DomainNotificationHelper.Validation;
using EP.CrudModalDDD.Domain.Entities;
using EP.CrudModalDDD.Domain.Interfaces.Repository;
using EP.CrudModalDDD.Domain.Scopes.ValueObjects;
using System;
using System.Linq;

namespace EP.CrudModalDDD.Domain.Scopes.Entities
{
    public static class ClienteScopes
    {
        #region Specifications Granulares

        public static bool EhMaiorDeIdade(this Cliente cliente)
        {
            return cliente.DataNascimento == null || AssertionConcern.IsSatisfiedBy(
                       AssertionConcern.AssertTrue(DateTime.Now.Year - cliente.DataNascimento.Value.Year >= 18,
                           "Cliente não tem maioridade para cadastro."));
        }

        public static bool PossuiCpfUnico(this Cliente cliente, IClienteRepository clienteRepository)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(clienteRepository.ObterPorCpf(cliente.CPF.Numero) == null,
                    "CPF já cadastrado! Esqueceu sua senha?")
            );
        }

        public static bool PossuiEmailUnico(this Cliente cliente, IClienteRepository clienteRepository)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(clienteRepository.ObterPorEmail(cliente.Email.Address) == null,
                    "E-mail já cadastrado! Esqueceu sua senha?")
            );
        }

        public static bool PossuiEnderecoInformado(this Cliente cliente)
        {
            return AssertionConcern.IsSatisfiedBy(
                AssertionConcern.AssertTrue(cliente.Enderecos != null && cliente.Enderecos.Any(),
                    "Cliente não informou endereço")
            );
        }

        #endregion Specifications Granulares

        #region Specifications Compostas

        public static bool EstaConsistente(this Cliente cliente)
        {
            return cliente.CPF.IsValid()
                   & cliente.Email.IsValid()
                   & cliente.EhMaiorDeIdade();
        }

        public static bool EstaAptoParaCadastro(this Cliente cliente, IClienteRepository clienteRepository)
        {
            return cliente.PossuiCpfUnico(clienteRepository) &
                   cliente.PossuiEmailUnico(clienteRepository) &
                   cliente.PossuiEnderecoInformado();
        }
        #endregion

        public static bool RegistrarNovoScopeEstaValido(this Cliente cliente)
        {
            return cliente.EstaConsistente() & cliente.PossuiEnderecoInformado();
        }
    }
}