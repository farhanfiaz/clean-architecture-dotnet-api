using CommonCore.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreDI(this IServiceCollection service, IConfiguration config)
    {
        service.Configure<ConnectionStringOptions>(config.GetSection(ConnectionStringOptions.SectionName));
        return service;
    }
}
