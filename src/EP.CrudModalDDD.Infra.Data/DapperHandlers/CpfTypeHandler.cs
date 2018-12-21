using Dapper;
using EP.CrudModalDDD.Domain.ValueObjects;
using System.Data;

namespace EP.CrudModalDDD.Infra.Data.DapperHandlers
{
    public class CpfTypeHandler : SqlMapper.TypeHandler<CPF>
    {
        public override void SetValue(IDbDataParameter parameter, CPF value)
        {
            parameter.Value = value.Numero;
        }

        public override CPF Parse(object value)
        {
            return new CPF((string)value);
        }
    }
}
