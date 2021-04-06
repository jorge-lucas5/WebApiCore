using System;
using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Estudos.App.WebApi.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLogginConfig(this IServiceCollection services)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "36c521f15c2f4ac2a9478581a7015e28";
                o.LogId = new Guid("061c1f6e-6e53-466b-a047-c1cdd28dcfea");
            });

            services.AddLogging(builder =>
            {
                builder.AddElmahIo(o =>
                {
                    o.ApiKey = "36c521f15c2f4ac2a9478581a7015e28";
                    o.LogId = new Guid("061c1f6e-6e53-466b-a047-c1cdd28dcfea");
                });

                builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            });
            return services;
        }

        public static IApplicationBuilder UseLogginConfig(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}