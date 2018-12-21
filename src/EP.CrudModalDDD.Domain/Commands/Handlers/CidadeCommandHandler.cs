using DomainNotificationHelper;
using DomainNotificationHelper.Events;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Commands.Results;
using EP.CrudModalDDD.Domain.Entities;
using EP.CrudModalDDD.Domain.Interfaces;
using EP.CrudModalDDD.Domain.Interfaces.Repository;
using EP.CrudModalDDD.Domain.Scopes.Commands;

namespace EP.CrudModalDDD.Domain.Commands.Handlers
{
    public class CidadeCommandHandler : CommandHandler, ICommandHandler<AdicionaNovaCidadeCommand>, ICommandHandler<AtualizaCidadeCommand>, ICommandHandler<RemoveCidadeCommand>
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeCommandHandler(IUnitOfWork uow, IHandler<DomainNotification> notifications,
            ICidadeRepository cidadeRepository) : base(uow, notifications)
        {
            _cidadeRepository = cidadeRepository;
        }

        public ICommandResult Handle(AdicionaNovaCidadeCommand command)
        {
            //Gera os Value Objects

            //Valida propriedades que não buscam de repositório
            if (!(command.PossuiNomeCidadeInformado()))
            {
                return new CidadeCommandResult();
            }
            //Valida propriedades que buscam de repositório
            if (!(command.PossuiNomeCidadeUnico(_cidadeRepository)))
            {
                return new CidadeCommandResult();
            }

            //Gera a entidade cidade
            var cidade = new Cidade(command.Id,command.Nome, command.UF, command.CodigoIbge);
        
            //Adiciona a entidade ao repositório
            _cidadeRepository.Adicionar(cidade);

            return new CidadeCommandResult();
        }

        public ICommandResult Handle(AtualizaCidadeCommand command)
        {
            //Valida propriedades que não buscam de repositório
            if (!(command.PossuiNomeCidadeInformado()))
            {
                return new CidadeCommandResult();
            }
        
            //Gera a entidade cidade
            var cidade = new Cidade(command.Id, command.Nome, command.UF, command.CodigoIbge);

            //Atualiza a entidade junto ao repositório
            _cidadeRepository.Atualizar(cidade);

            return new CidadeCommandResult();
        }

        public ICommandResult Handle(RemoveCidadeCommand command)
        {
           
            _cidadeRepository.Remover(command.Id);

            return new CidadeCommandResult();
        }
    }
}