﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(CarWaterless.Startup))]

namespace CarWaterless
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            
            //  app.MapSignalR(new HubConfiguration { EnableJSONP = true });
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration { };
                // GlobalHost.DependencyResolver.UseRedis("cinema.redis.cache.windows.net", 6379, "SQw3hWOSaIzkqQKr+ZWsW4Hh17grjV+lo84H8Ph+19U=", "Careme");

                map.RunSignalR(hubConfiguration);
            });
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
