using CommonCore.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Application.Events.User;

public record UserCreatedEvent(AspNetUser user)
: INotification;
