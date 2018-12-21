using AutoMapper;
using EP.CrudModalDDD.Application.Interfaces;
using EP.CrudModalDDD.Application.ViewModels;
using EP.CrudModalDDD.Domain.Commands.Handlers;
using EP.CrudModalDDD.Domain.Commands.Inputs;
using EP.CrudModalDDD.Domain.Entities;
using EP.CrudModalDDD.Domain.Interfaces;
using EP.CrudModalDDD.Domain.Interfaces.Repository;
using System;
using System.IO;
using System.Web;

namespace EP.CrudModalDDD.Application
{
    public class ClienteAppService : ApplicationService, IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ICommandHandler<AdicionaNovoClienteCommand> _handlerAdicionaNovoCliente;
        private readonly ICommandHandler<AtualizaClienteCommand> _handlerAtualizaCliente;
        private readonly ICommandHandler<RemoveClienteCommand> _handlerRemoveCliente;
        private readonly ICommandHandler<AdicionaNovoEnderecoCommand> _handlerAdicionaNovoEndereco;
        private readonly ICommandHandler<AtualizaEnderecoCommand> _handlerAtualizaEndereco;
        private readonly ICommandHandler<RemoveEnderecoCommand> _handlerRemoveEndereco;

        public ClienteAppService(IClienteRepository clienteRepository, IEnderecoRepository enderecoRepository,
            IUnitOfWork unitOfWork,
            ICommandHandler<AdicionaNovoClienteCommand> handlerAdicionaNovoCliente,
            ICommandHandler<AtualizaClienteCommand> handlerAtualizaCliente,
            ICommandHandler<RemoveClienteCommand> handlerRemoveCliente,
            ICommandHandler<AdicionaNovoEnderecoCommand> handlerAdicionaNovoEndereco,
            ICommandHandler<AtualizaEnderecoCommand> handlerAtualizaEndereco,
            ICommandHandler<RemoveEnderecoCommand> handlerRemoveEndereco)
            : base(unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
            _handlerAdicionaNovoCliente = handlerAdicionaNovoCliente;
            _handlerAtualizaCliente = handlerAtualizaCliente;
            _handlerRemoveCliente = handlerRemoveCliente;
            _handlerAdicionaNovoEndereco = handlerAdicionaNovoEndereco;
            _handlerAtualizaEndereco = handlerAtualizaEndereco;
            _handlerRemoveEndereco = handlerRemoveEndereco;
        }

        public ClienteEnderecoViewModel Adicionar(ClienteEnderecoViewModel clienteEnderecoViewModel)
        {
            //** ANTES
            //var cliente = Mapper.Map<Cliente>(clienteEnderecoViewModel);
            //var endereco = Mapper.Map<Endereco>(clienteEnderecoViewModel);

            //cliente.AdicionarEndereco(endereco);

            //var clienteReturn = _clienteService.Adicionar(cliente);
            //clienteEnderecoViewModel = Mapper.Map<ClienteEnderecoViewModel>(clienteReturn);

            //Commit();

            //return clienteEnderecoViewModel;

            //** DEPOIS
            var clienteCommand = Mapper.Map<AdicionaNovoClienteCommand>(clienteEnderecoViewModel);
            var enderecoCommand = Mapper.Map<ClienteEnderecoCommand>(clienteEnderecoViewModel);

            clienteCommand.Enderecos.Add(enderecoCommand);

            var result = _handlerAdicionaNovoCliente.Handle(clienteCommand);

            Commit();

            return clienteEnderecoViewModel;
        }

        public ClienteViewModel ObterPorId(Guid id)
        {
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorId(id));
        }

        public ClienteViewModel ObterPorCpf(string cpf)
        {
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorCpf(cpf));
        }

        public ClienteViewModel ObterPorEmail(string email)
        {
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorEmail(email));
        }

        public PagedViewModel<ClienteViewModel> ObterTodos(string nome, int pageSize, int pageNumber)
        {
            return Mapper.Map<PagedViewModel<ClienteViewModel>>(
                _clienteRepository.ObterTodos(nome, pageSize, pageNumber));
        }

        public ClienteViewModel Atualizar(ClienteViewModel clienteViewModel)
        {
            //**ANTES
            //BeginTransaction();
            //_clienteService.Atualizar(Mapper.Map<Cliente>(clienteViewModel));
            //Commit();

            //**DEPOIS
            var clienteCommand = Mapper.Map<AtualizaClienteCommand>(clienteViewModel);

            var result = _handlerAtualizaCliente.Handle(clienteCommand);

            Commit();

            return clienteViewModel;
        }

        public void Remover(Guid id)
        {
            //**ANTES
            //BeginTransaction();
            //_clienteService.Remover(id);
            //Commit();

            //**DEPOIS
            var clienteCommand = new RemoveClienteCommand(id);

            var result = _handlerRemoveCliente.Handle(clienteCommand);

            Commit();
        }

        public EnderecoViewModel AdicionarEndereco(EnderecoViewModel enderecoViewModel)
        {
            //** ANTES
            //var endereco = Mapper.Map<Endereco>(enderecoViewModel);

            //BeginTransaction();
            //_clienteService.AdicionarEndereco(endereco);
            //Commit();

            //return enderecoViewModel;

            //**DEPOIS
            var enderecoCommand = Mapper.Map<AdicionaNovoEnderecoCommand>(enderecoViewModel);

            var result = _handlerAdicionaNovoEndereco.Handle(enderecoCommand);

            Commit();

            return enderecoViewModel;
        }

        public EnderecoViewModel AtualizarEndereco(EnderecoViewModel enderecoViewModel)
        {
            //** ANTES
            //var endereco = Mapper.Map<Endereco>(enderecoViewModel);

            //BeginTransaction();
            //_clienteService.AtualizarEndereco(endereco);
            //Commit();

            //return enderecoViewModel;

            //** DEPOIS
            var enderecoCommand = Mapper.Map<AtualizaEnderecoCommand>(enderecoViewModel);

            var result = _handlerAtualizaEndereco.Handle(enderecoCommand);

            Commit();

            return enderecoViewModel;
        }

        public EnderecoViewModel ObterEnderecoPorId(Guid id)
        {
            return Mapper.Map<EnderecoViewModel>(_enderecoRepository.ObterPorId(id));
        }

        public void RemoverEndereco(Guid id)
        {
            //** ANTES
            //BeginTransaction();
            //_clienteService.RemoverEndereco(id);
            //Commit();

            //** DEPOIS
            var enderecoCommand = new RemoveEnderecoCommand(id);

            var result = _handlerRemoveEndereco.Handle(enderecoCommand);

            Commit();
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
            _enderecoRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        private static bool SalvarImagemCliente(HttpPostedFileBase img, Guid id)
        {
            if (img == null || img.ContentLength <= 0) return false;

            const string directory = @"D:\Labs\CursoMVC Update\src\contents\clientes\";
            var fileName = id + Path.GetExtension(img.FileName);
            img.SaveAs(Path.Combine(directory, fileName));
            return File.Exists(Path.Combine(directory, fileName));
        }
    }
}