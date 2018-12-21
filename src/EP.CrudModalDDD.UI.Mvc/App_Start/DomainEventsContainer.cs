using System;
using System.Collections.Generic;
using DomainNotificationHelper;
using SimpleInjector.Integration.Web.Mvc;

namespace EP.CrudModalDDD.UI.Mvc
{
    public class DomainEventsContainer: IContainer
    {
        private readonly SimpleInjectorDependencyResolver _resolver;

        public DomainEventsContainer(SimpleInjectorDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            return _resolver.GetService(serviceType);
        }

        public T GetService<T>()
        {
            return (T) _resolver.GetService(typeof(T));
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolver.GetServices(serviceType);
        }

        public IEnumerable<T> GetServices<T>()
        {
            return (IEnumerable<T>) _resolver.GetServices(typeof(T));
        }
    }
}