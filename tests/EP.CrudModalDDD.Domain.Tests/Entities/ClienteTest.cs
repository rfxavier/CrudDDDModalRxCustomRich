using EP.CrudModalDDD.Domain.Entities;
using EP.CrudModalDDD.Domain.Scopes.Entities;
using EP.CrudModalDDD.Domain.Scopes.ValueObjects;
using EP.CrudModalDDD.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EP.CrudModalDDD.Domain.Tests.Entities
{
    [TestClass]
    public class ClienteTest
    {
        //dummy
        public Cliente Cliente { get; set; }
        public Endereco Endereco { get; set; }

        //private readonly List<DomainNotification> _notifications = new List<DomainNotification>();

        [TestMethod]
        [TestCategory("Cliente")]
        public void DadoUmClienteNovoComDataNascimentoDeveRetornarSucesso()
        {
            string nome = "";
            EmailAddress email = new EmailAddress("cliente@cliente.com.br");
            CPF cpf = new CPF("30390600822");
            DateTime? dataNascimento = new DateTime(1972, 4, 13);
            bool ativo = false;

            Cliente = new Cliente(Guid.NewGuid(), nome, email, cpf, dataNascimento, ativo);

            string logradouro = "";
            string numero = "";
            string complemento = "";
            string bairro = "";
            string cep = "";
            string cidade = "";
            string estado = "";

            Endereco = new Endereco(logradouro, numero, complemento, bairro, cep, cidade, estado);

            Cliente.AdicionarEndereco(Endereco);

            //Assert.IsTrue(Cliente.CPF.IsValid());
            //Assert.IsTrue(Cliente.Email.IsValid());
            //Assert.IsTrue(Cliente.EhMaiorDeIdade());

            //Assert.IsTrue(Cliente.PossuiEnderecoInformado());

            Assert.IsTrue(Cliente.RegistrarNovo());
        }

        [TestMethod]
        [TestCategory("Cliente")]
        public void DadoUmClienteNovoSemDataNascimentoDeveRetornarSucesso()
        {
            string nome = "";
            EmailAddress email = new EmailAddress("cliente@cliente.com.br");
            CPF cpf = new CPF("30390600822");
            DateTime? dataNascimento = null;
            bool ativo = false;

            Cliente = new Cliente(Guid.NewGuid(), nome, email, cpf, dataNascimento, ativo);

            string logradouro = "";
            string numero = "";
            string complemento = "";
            string bairro = "";
            string cep = "";
            string cidade = "";
            string estado = "";

            Endereco = new Endereco(logradouro, numero, complemento, bairro, cep, cidade, estado);

            Cliente.AdicionarEndereco(Endereco);

            Assert.IsTrue(Cliente.CPF.IsValid());
            Assert.IsTrue(Cliente.Email.IsValid());
            Assert.IsTrue(Cliente.EhMaiorDeIdade());

            Assert.IsTrue(Cliente.PossuiEnderecoInformado());

            Assert.IsTrue(Cliente.RegistrarNovo());
        }

        [TestMethod]
        [TestCategory("Cliente")]
        public void DadoUmClienteNovoDeveRetornarQueNaoPodeRegistrarETodasAsNotificacoesPossiveis()
        {
            string nome = "";
            EmailAddress email = new EmailAddress("cliente2cliente.com.br");
            CPF cpf = new CPF("30390600821");
            DateTime? dataNascimento = new DateTime(2005, 01, 01);
            bool ativo = false;

            Cliente = new Cliente(Guid.NewGuid(), nome, email, cpf, dataNascimento, ativo);

            Assert.IsFalse(Cliente.CPF.IsValid());
            Assert.IsFalse(Cliente.Email.IsValid());
            Assert.IsFalse(Cliente.EhMaiorDeIdade());

            Assert.IsFalse(Cliente.PossuiEnderecoInformado());

            Assert.IsFalse(Cliente.RegistrarNovo());
        }



        //[TestMethod]
        //public void ClienteConsistente_Valid_False()
        //{
        //    //DomainEvents.ClearCallbacks();
        //    //DomainEvents.Register<DomainNotification>(e => _notifications.Add(e));

        //    Cliente = new Cliente
        //    {
        //        CPF = new CPF("30390600821"),
        //        DataNascimento = new DateTime(2005, 01, 01),
        //        Email = new EmailAddress("cliente2cliente.com.br")
        //    };

        //    Assert.IsFalse(Cliente.EstaConsistente());

        //    //Assert.IsTrue(_notifications.Any(e => e.Value == "CPF deve ser válido"));
        //    //Assert.IsTrue(_notifications.Any(e => e.Value == "E-mail está inválido."));
        //    //Assert.IsTrue(_notifications.Any(e => e.Value == "Cliente não tem maioridade para cadastro."));
        //}

        //[TestMethod]
        //public void ClienteEstaAptoParaCadastro_Valid_True()
        //{
        //    Cliente = new Cliente()
        //    {
        //        CPF = new CPF("30390600822"),
        //        Email = new EmailAddress("edu@edu.com.br")
        //    };

        //    Cliente.Enderecos.Add(new Endereco());

        //    var stubRepo = MockRepository.GenerateStub<IClienteRepository>();
        //    stubRepo.Stub(s => s.ObterPorEmail(Cliente.Email.Address)).Return(null);
        //    stubRepo.Stub(s => s.ObterPorCpf(Cliente.CPF.Numero)).Return(null);

        //    Assert.IsTrue(Cliente.EstaAptoParaCadastro(stubRepo));
        //}

        //[TestMethod]
        //public void ClienteEstaAptoParaCadastro_Valid_False()
        //{
        //    //DomainEvents.ClearCallbacks();
        //    //DomainEvents.Register<DomainNotification>(e => _notifications.Add(e));

        //    Cliente = new Cliente()
        //    {
        //        CPF = new CPF("30390600822"),
        //        Email = new EmailAddress("edu@edu.com.br")
        //    };

        //    var stubRepo = MockRepository.GenerateStub<IClienteRepository>();
        //    stubRepo.Stub(s => s.ObterPorEmail(Cliente.Email.Address)).Return(Cliente);
        //    stubRepo.Stub(s => s.ObterPorCpf(Cliente.CPF.Numero)).Return(Cliente);

        //    Assert.IsFalse(Cliente.EstaAptoParaCadastro(stubRepo));

        //    //Assert.IsTrue(_notifications.Any(e => e.Value == "Cliente não informou endereço"));
        //    //Assert.IsTrue(_notifications.Any(e => e.Value == "CPF já cadastrado! Esqueceu sua senha?"));
        //    //Assert.IsTrue(_notifications.Any(e => e.Value == "E-mail já cadastrado! Esqueceu sua senha?"));
        //}
    }
}