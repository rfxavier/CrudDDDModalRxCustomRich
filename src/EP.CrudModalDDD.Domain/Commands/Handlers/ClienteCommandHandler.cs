using System;
using DomainNotificationHelper;
using DomainNotificationHelper.Events;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Commands.Results;
using EP.CrudModalDDD.Domain.Entities;
using EP.CrudModalDDD.Domain.Interfaces;
using EP.CrudModalDDD.Domain.Interfaces.Repository;
using EP.CrudModalDDD.Domain.Scopes.Commands;
using EP.CrudModalDDD.Domain.Scopes.ValueObjects;
using EP.CrudModalDDD.Domain.ValueObjects;

namespace EP.CrudModalDDD.Domain.Commands.Handlers
{
    public class ClienteCommandHandler : CommandHandler, 
        ICommandHandler<AdicionaNovoClienteCommand>,
        ICommandHandler<AtualizaClienteCommand>,
        ICommandHandler<RemoveClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IUnitOfWork uow, IHandler<DomainNotification> notifications,
            IClienteRepository clienteRepository) : base(uow, notifications)
        {
            _clienteRepository = clienteRepository;
        }

        public ICommandResult Handle(AdicionaNovoClienteCommand command)
        {
            //Gera os Value Objects
            var cpf = new CPF(command.CpfNumero);
            var emailAddress = new EmailAddress(command.EmailAddress);

            //Valida propriedades que não buscam de repositório
            if (!(cpf.IsValid() & emailAddress.IsValid() & command.EhMaiorDeIdade() &
                  command.PossuiEnderecoInformado()))
            {
                return new ClienteCommandResult();
            }

            //Valida propriedades que buscam de repositório
            if (!(command.PossuiCpfUnico(_clienteRepository) & command.PossuiEmailUnico(_clienteRepository)))
            {
                return new ClienteCommandResult();
            }

            //Gera a entidade cliente
            var cliente = new Cliente(command.ClienteId, command.Nome, emailAddress, cpf, command.DataNascimento, command.Ativo);

            //Gera as 
            foreach (var clienteEnderecoCommand in command.Enderecos)
            {
                var endereco = new Endereco(clienteEnderecoCommand.Logradouro, clienteEnderecoCommand.Numero,
                    clienteEnderecoCommand.Complemento, clienteEnderecoCommand.Bairro, clienteEnderecoCommand.CEP,
                    clienteEnderecoCommand.Cidade, clienteEnderecoCommand.Estado);

                cliente.AdicionarEndereco(endereco);
            }

            //Adiciona a entidade ao repositório
            _clienteRepository.Adicionar(cliente);

            return new ClienteCommandResult();
        }

        public ICommandResult Handle(AtualizaClienteCommand command)
        {
            //Gera os Value Objects
            var cpf = new CPF(command.CpfNumero);
            var emailAddress = new EmailAddress(command.EmailAddress);

            //Valida propriedades que não buscam de repositório
            if (!(cpf.IsValid() & emailAddress.IsValid() & command.EhMaiorDeIdade()))
            {
                return new ClienteCommandResult();
            }

            //Valida propriedades que buscam de repositório
            if (!(command.PossuiCpfUnico(_clienteRepository) & command.PossuiEmailUnico(_clienteRepository)))
            {
                return new ClienteCommandResult();
            }

            //Gera a entidade cliente
            var cliente = new Cliente(command.ClienteId, command.Nome, emailAddress, cpf, command.DataNascimento, command.Ativo);

            //Atualiza a entidade junto ao repositório
            _clienteRepository.Atualizar(cliente);

            return new ClienteCommandResult();
        }

        public ICommandResult Handle(RemoveClienteCommand command)
        {
            if (!command.PossuiEnderecoInformado(_clienteRepository))
            {
                return new ClienteCommandResult();
            }

            _clienteRepository.Remover(command.ClienteId);

            return new ClienteCommandResult();
        }
    }
}