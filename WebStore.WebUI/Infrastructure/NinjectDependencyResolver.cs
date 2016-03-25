using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ninject;

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
        private void AddBindings()
        {
            //put buindings here
        }
    }
}
