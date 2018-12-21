using DomainNotificationHelper;
using DomainNotificationHelper.Events;
using EP.CrudModalDDD.Domain.Interfaces;

namespace EP.CrudModalDDD.Domain.Commands.Handlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IHandler<DomainNotification> _notifications;

        public CommandHandler(IUnitOfWork uow, IHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = notifications;
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            var commandResponse = _uow.Commit();
            if (commandResponse.Success) return true;

            return false;
        }
    }
}