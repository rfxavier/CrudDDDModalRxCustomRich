using Dapper;
using EP.CrudModalDDD.Domain.ValueObjects;
using System;
using System.Data;

namespace EP.CrudModalDDD.Infra.Data.DapperHandlers
{
    public class EmailAddressTypeHandler : SqlMapper.TypeHandler<EmailAddress>
    {
        public override void SetValue(IDbDataParameter parameter, EmailAddress value)
        {
            parameter.Value = value.Address;
        }

        public override EmailAddress Parse(object value)
        {
            return new EmailAddress((string)value);
        }
    }
}
