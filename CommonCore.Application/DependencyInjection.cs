using CommonCore.Application.Behaviors;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection service)
    {
        service.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            //cfg.NotificationPublisher = new TaskWhenAllPublisher();
            cfg.NotificationPublisher = new ForeachAwaitPublisher();
        });

        service.AddTransient(
           typeof(IPipelineBehavior<,>),
           typeof(LoggingBehavior<,>));

        return service;
    }
}
