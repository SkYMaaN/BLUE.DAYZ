using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace BLUE.API.Helpers
{
    public class DependencyInjectionHttpControllerActivator : IHttpControllerActivator
    {
        private readonly ServiceProvider provider;

        public DependencyInjectionHttpControllerActivator(ServiceProvider provider)
        {
            this.provider = provider;
        }

        public IHttpController Create(
            HttpRequestMessage request, HttpControllerDescriptor descriptor, Type type)
        {
            var scope = this.provider.CreateScope();
            request.RegisterForDispose(scope);
            return (IHttpController)scope.ServiceProvider.GetRequiredService(type);
        }
    }
}