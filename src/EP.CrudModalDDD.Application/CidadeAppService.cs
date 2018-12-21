using AutoMapper;
using EP.CrudModalDDD.Application.Interfaces;
using EP.CrudModalDDD.Application.ViewModels;
using EP.CrudModalDDD.Domain.Commands.Handlers;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Interfaces;
using EP.CrudModalDDD.Domain.Interfaces.Repository;
using System;

namespace EP.CrudModalDDD.Application
{
    public class CidadeAppService : ApplicationService, ICidadeAppService
    {
        private readonly ICidadeRepository _cidadeRepository;
        private readonly ICommandHandler<AdicionaNovaCidadeCommand> _handlerAdicionaNovaCidade;
        private readonly ICommandHandler<AtualizaCidadeCommand> _handlerAtualizaCidade;
        private readonly ICommandHandler<RemoveCidadeCommand> _handlerRemovecidade;

        public CidadeAppService(ICidadeRepository cidadeRepository, IUnitOfWork unitOfWork,
            ICommandHandler<AdicionaNovaCidadeCommand> handlerAdicionaNovaCidade,
            ICommandHandler<AtualizaCidadeCommand> handlerAtualizaCidade,
            ICommandHandler<RemoveCidadeCommand> handlerRemoveCidade) : base(unitOfWork)
        {
            _cidadeRepository = cidadeRepository;
            _handlerAdicionaNovaCidade = handlerAdicionaNovaCidade;
            _handlerAtualizaCidade = handlerAtualizaCidade;
            _handlerRemovecidade = handlerRemoveCidade;
        }

        public CidadeViewModel Adicionar(CidadeViewModel cidadeViewModel)
        {
            var cidadeCommand = Mapper.Map<AdicionaNovaCidadeCommand>(cidadeViewModel);

            var result = _handlerAdicionaNovaCidade.Handle(cidadeCommand);

            Commit();

            return cidadeViewModel;
        }

        public CidadeViewModel ObterPorId(Guid id)
        {
            return Mapper.Map<CidadeViewModel>(_cidadeRepository.ObterPorId(id));
        }

        //public IEnumerable<CidadeViewModel> ObterTodos()
        //{
        //    return Mapper.Map<IEnumerable<CidadeViewModel>>(_cidadeRepository.ObterTodos());
        //}

        public PagedViewModel<CidadeViewModel> ObterTodos(string nome, int pageSize, int pageNumber)
        {
            return Mapper.Map<PagedViewModel<CidadeViewModel>>(_cidadeRepository.ObterTodos(nome, pageSize, pageNumber));
        }

        public CidadeViewModel Atualizar(CidadeViewModel cidadeViewModel)
        {
            var cidadeCommand = Mapper.Map<AtualizaCidadeCommand>(cidadeViewModel);

            var result = _handlerAtualizaCidade.Handle(cidadeCommand);

            Commit();

            return cidadeViewModel;
        }

        public void Remover(Guid id)
        {
            //**DEPOIS
            var cidadeCommand = new RemoveCidadeCommand(id);

            var result = _handlerRemovecidade.Handle(cidadeCommand);

            Commit();
        }

        public void Dispose()
        {
            _cidadeRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}