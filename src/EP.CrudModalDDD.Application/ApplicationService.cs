using DomainNotificationHelper;
using DomainNotificationHelper.Events;
using EP.CrudModalDDD.Domain.Interfaces;

namespace EP.CrudModalDDD.Application
{
    public abstract class ApplicationService
    {
        private readonly IHandler<DomainNotification> _notifications;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
        }

        public void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications())
                return false;

            _unitOfWork.Commit();

            return true;
        }

        public void Rollback(string message)
        {
            DomainEvent.Raise<DomainNotification>(new DomainNotification("BusinessError", message));
            _unitOfWork.Rollback();
        }

        public void Rollback()
        {
            _unitOfWork.Rollback();
        }
    }
}