using EP.CrudModalDDD.Domain.Commands.Results;
using System;

namespace EP.CrudModalDDD.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
        void Rollback();
        void BeginTransaction();
    }
}
