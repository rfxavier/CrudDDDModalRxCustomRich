using DomainNotificationHelper.Validation;
using EP.CrudModalDDD.Domain.ValueObjects;

namespace EP.CrudModalDDD.Domain.Scopes.ValueObjects
{
    public static class CPFScopes
    {
        public static bool IsValid(this CPF cpf)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertTrue(ValidaCPF(cpf), "CPF deve ser válido")
            );
        }

        private static bool ValidaCPF(CPF cpf)
        {
            var cpfNumero = cpf.Numero;

            if (cpf.Numero == null)
                return false;

            if (cpfNumero.Length > 11)
                return false;

            while (cpfNumero.Length != 11)
                cpfNumero = '0' + cpfNumero;

            var igual = true;
            for (var i = 1; i < 11 && igual; i++)
                if (cpfNumero[i] != cpfNumero[0])
                    igual = false;

            if (igual || cpfNumero == "12345678909")
                return false;

            var numeros = new int[11];

            for (var i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpfNumero[i].ToString());

            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            var resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }
    }
}
