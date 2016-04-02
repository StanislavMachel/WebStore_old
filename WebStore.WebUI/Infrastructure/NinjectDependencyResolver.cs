using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ninject;
using WebStore.Domain.Abstract;
using WebStore.Repository;
using WebStore.Domain.Entities;
using System.Configuration;

namespace WebStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kelner;
        public NinjectDependencyResolver(IKernel kelnerParam)
        {
            kelner = kelnerParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kelner.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kelner.GetAll(serviceType);
        }

        EmailSettings emailSettings = new EmailSettings
        {
            WrireAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile" ?? "false"])
        };
        private void AddBindings()
        {
            //put buindings here
            kelner.Bind<IProductRepository>()
                  .To<SimpleProductRepository>();
            kelner.Bind<IOrderProcessor>()
                  .To<EmailOrderProcessor>()
                  .WithConstructorArgument("settings", emailSettings);
        }
    }
}
