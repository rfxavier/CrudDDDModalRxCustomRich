using DomainNotificationHelper;
using DomainNotificationHelper.Events;
using DomainNotificationHelper.Handlers;
using EP.CrudModalDDD.Application;
using EP.CrudModalDDD.Application.Interfaces;
using EP.CrudModalDDD.Domain.Commands.Handlers;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Interfaces;
using EP.CrudModalDDD.Domain.Interfaces.Repository;
using EP.CrudModalDDD.Infra.CrossCutting.Logging.Data;
using EP.CrudModalDDD.Infra.CrossCutting.Logging.Helpers;
using EP.CrudModalDDD.Infra.Data.Context;
using EP.CrudModalDDD.Infra.Data.Repository;
using EP.CrudModalDDD.Infra.Data.UoW;
using SimpleInjector;

namespace EP.CrudModalDDD.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            // Lifestyle.Transient => Uma instancia para cada solicitacao;
            // Lifestyle.Singleton => Uma instancia unica para a classe
            // Lifestyle.Scoped => Uma instancia unica para o request

            // App
            container.Register<IHandler<DomainNotification>, DomainNotificationHandler>(Lifestyle.Scoped);

            container.Register<IClienteAppService, ClienteAppService>(Lifestyle.Scoped);
            container.Register<ICommandHandler<AdicionaNovoClienteCommand>, ClienteCommandHandler>(Lifestyle.Scoped);
            container.Register<ICommandHandler<AtualizaClienteCommand>, ClienteCommandHandler>(Lifestyle.Scoped);
            container.Register<ICommandHandler<RemoveClienteCommand>, ClienteCommandHandler>(Lifestyle.Scoped);

            container.Register<ICommandHandler<AdicionaNovoEnderecoCommand>, EnderecoCommandHandler>(Lifestyle.Scoped);
            container.Register<ICommandHandler<AtualizaEnderecoCommand>, EnderecoCommandHandler>(Lifestyle.Scoped);
            container.Register<ICommandHandler<RemoveEnderecoCommand>, EnderecoCommandHandler>(Lifestyle.Scoped);

            container.Register<ICidadeAppService, CidadeAppService>(Lifestyle.Scoped);
            container.Register<ICommandHandler<AdicionaNovaCidadeCommand>, CidadeCommandHandler>(Lifestyle.Scoped);
            container.Register<ICommandHandler<AtualizaCidadeCommand>, CidadeCommandHandler>(Lifestyle.Scoped);
            container.Register<ICommandHandler<RemoveCidadeCommand>,CidadeCommandHandler>(Lifestyle.Scoped);

            // Domain
            //container.Register<IClienteService, ClienteService>(Lifestyle.Scoped);
            //container.Register<ICidadeService, CidadeService>(Lifestyle.Scoped);

            container.Register<IUnitOfWork, UnitOfWork<CrudModalDDDContext>>(Lifestyle.Scoped);

            // Infra Dados
            container.Register<IClienteRepository, ClienteRepository>(Lifestyle.Scoped);
            container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);

            container.Register<ICidadeRepository, CidadeRepository>(Lifestyle.Scoped);

            container.Register<CrudModalDDDContext>(Lifestyle.Scoped);
            //container.Register(typeof (IRepository<>), typeof (Repository<>));

            // Logging
            container.Register<ILogAuditoria, LogAuditoriaHelper>(Lifestyle.Scoped);
            container.Register<LogginContext>(Lifestyle.Scoped);
        }
    }
}