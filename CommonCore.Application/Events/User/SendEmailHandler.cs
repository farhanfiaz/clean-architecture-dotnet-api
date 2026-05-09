using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Application.Events.User;

internal class SendEmailHandler : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        await Task.Run(() => { });
    }
}
