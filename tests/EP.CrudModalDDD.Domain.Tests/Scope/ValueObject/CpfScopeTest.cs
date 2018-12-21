using EP.CrudModalDDD.Domain.Scopes.ValueObjects;
using EP.CrudModalDDD.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EP.CrudModalDDD.Domain.Tests.Scope.ValueObject
{
    [TestClass]
    public class CpfScopeTest
    {
        [TestMethod]
        public void CPF_Valido_True()
        {
            var cpf = new CPF("30390600822");

            Assert.IsTrue(cpf.IsValid());
        }

        [TestMethod]
        public void CPF_Valido_False()
        {
            var cpf = new CPF("30390600821");

            Assert.IsFalse(cpf.IsValid());
        }

    }
}
