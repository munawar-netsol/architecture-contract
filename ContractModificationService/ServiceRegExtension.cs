using Consul;

namespace ServiceRegistration
{
    public static class ServiceRegisterationExntension
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IConfiguration config)
        {
            var consulAddress = config.GetSection("Consul")["Url"];
            var serviceName = config.GetSection("MainService")["serviceName"];            
            var serviceId = config.GetSection("MainService")["serviceId"];    
            var url = config.GetSection("MainService")["url"];
            var uri = new Uri(url);
            var consulClient = new ConsulClient(x => x.Address = new Uri($"{consulAddress}"));//Consul address requesting registration          
            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                ID = serviceId,
                Name = config.GetSection("MainService")["serviceName"],
                Address = uri.Host,
                Port = uri.Port,
                Tags = new[] { $"urlprefix-/{serviceName}" }//Add a tag tag in the format of urlprefix-/servicename so that Fabio can recognize it
            };
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();//Unregister when the service stops
            consulClient.Agent.ServiceRegister(registration).Wait();//Register when the service starts, the internal implementation is actually to register using the Consul API (initiated by HttpClient)
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//Unregister when the service stops
            });

            return app;
        }
    }
}