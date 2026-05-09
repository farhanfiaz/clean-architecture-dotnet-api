using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Application.Commands.AuthToken;

public record AddAuthTokenCommand(SingleDeviceDto model, CancellationToken cancellationToken) : IRequest<ApiResult>;
