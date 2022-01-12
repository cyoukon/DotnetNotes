using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SocketLib
{
    public static class WebSocketExtensions
    {
        public static IServiceCollection AddWebSocket(this IServiceCollection services)
        {
            services.AddSingleton<ISocketFactory, SocketFactory>();

            var exportedTypes = Assembly.GetEntryAssembly()?.ExportedTypes;
            if (exportedTypes == null) return services;

            foreach (var type in exportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
                {
                    services.AddSingleton(type);
                }
            }

            return services;
        }

        public static IApplicationBuilder MapWebSockets<T>(this IApplicationBuilder app, PathString path) where T : SocketHandler, new()
        {
            var socket = app.ApplicationServices.GetRequiredService<T>();
            return app.Map(path, x => x.UseMiddleware<SocketsMiddleware>(socket));
        }
    }
}
