using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace UnitTest
{
    public class DependencyResolverHelper
    {
        private readonly IWebHost _webHost;

        /// <summary>
        /// This class to help handle all DI for MVC Web App
        /// </summary>
        /// <param name="WebHost"></param>
        public DependencyResolverHelper(IWebHost WebHost) => _webHost = WebHost;

        public T GetService<T>()
        {
            using (var serviceScope = _webHost.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var scopedService = services.GetRequiredService<T>();
                    return scopedService;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            };
        }
    }
}
