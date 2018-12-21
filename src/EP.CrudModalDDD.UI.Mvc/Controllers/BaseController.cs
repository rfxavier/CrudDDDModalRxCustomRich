using System.Web.Mvc;
using DomainNotificationHelper;
using DomainNotificationHelper.Events;
using Microsoft.AspNet.Identity;

namespace EP.CrudModalDDD.UI.Mvc.Controllers
{
    public class BaseController : Controller
    {
        public string UserId
        {
            get
            {
                return ControllerContext.HttpContext.User.Identity.IsAuthenticated ? ControllerContext.HttpContext.User.Identity.GetUserId() : "0";
            }
        }

        public const int PageSize = 5;

        public IHandler<DomainNotification> Notifications;

        public BaseController()
        {
            Notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
        }
    }
}