using EP.CrudModalDDD.Domain.Commands.Results;
using EP.CrudModalDDD.Domain.Interfaces;
using System;
using System.Data.Entity;

namespace EP.CrudModalDDD.Infra.Data.UoW
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext _context;
        private bool _disposed;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        CommandResponse IUnitOfWork.Commit()
        {
            _context.SaveChanges();
            return CommandResponse.Ok;
        }
    }
}