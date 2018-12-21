using DomainNotificationHelper;
using DomainNotificationHelper.Events;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Commands.Results;
using EP.CrudModalDDD.Domain.Entities;
using EP.CrudModalDDD.Domain.Interfaces;
using EP.CrudModalDDD.Domain.Interfaces.Repository;

namespace EP.CrudModalDDD.Domain.Commands.Handlers
{
    public class EnderecoCommandHandler : CommandHandler, ICommandHandler<AdicionaNovoEnderecoCommand>,
        ICommandHandler<AtualizaEnderecoCommand>, ICommandHandler<RemoveEnderecoCommand>
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoCommandHandler(IUnitOfWork uow, IHandler<DomainNotification> notifications,
            IEnderecoRepository enderecoRepository) : base(uow, notifications)
        {
            _enderecoRepository = enderecoRepository;
        }

        public ICommandResult Handle(AdicionaNovoEnderecoCommand command)
        {
            //Gera os Value Objects

            //Valida propriedades que não buscam de repositório

            //Valida propriedades que buscam de repositório

            //Gera a entidade endereco
            var endereco = new Endereco(command.ClienteId,command.EnderecoId, command.Logradouro, command.Numero, command.Complemento, command.Bairro,
                command.CEP, command.Cidade, command.Estado);

            //Adiciona a entidade ao repositório
            _enderecoRepository.Adicionar(endereco);

            return new EnderecoCommandResult();
        }

        public ICommandResult Handle(AtualizaEnderecoCommand command)
        {
            //Gera os Value Objects

            //Valida propriedades que não buscam de repositório

            //Valida propriedades que buscam de repositório

            //Gera a entidade endereco
            var endereco = new Endereco(command.ClienteId, command.EnderecoId, command.Logradouro, command.Numero, command.Complemento, command.Bairro,
                command.CEP, command.Cidade, command.Estado);

            //Adiciona a entidade ao repositório
            _enderecoRepository.Atualizar(endereco);

            return new EnderecoCommandResult();
        }

        public ICommandResult Handle(RemoveEnderecoCommand command)
        {
            _enderecoRepository.Remover(command.EnderecoId);

            return new EnderecoCommandResult();
        }
    }
}